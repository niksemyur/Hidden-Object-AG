using System.Collections.Generic;
using Zenject;
using HiddenObject.Item;
using HiddenObject.Configs;
using HiddenObject.Signals.Item;
using UnityEngine;

namespace HiddenObject.Managers
{
    public class LevelController: MonoBehaviour, IInitializable
    {
        [SerializeField] private ItemController[] LevelItems; //Все объекты для поиска на уровне

        [Inject] private readonly LevelConfig _levelConfig; 
        [Inject] private readonly SignalBus _signalBus;

        private ItemData[] _activeItems; //массив для более удобно работы со слотами в UI (к поиску доступны лишь объекты которые на UI)
        private Queue<ItemData> _itemQueue = new(); //очередь объектов которые пока не доступны для поиска

        public void Initialize()
        {
            _signalBus.Subscribe<ItemClickedSignal>(OnItemClicked);

            _activeItems = new ItemData[_levelConfig.MaxFindingSlotsCount];

            SetupItemQueue();
            FillEmptySlots();

        }

        //Заполнение очереди объектов для поиска
        private void SetupItemQueue()
        {
            foreach (var item in _levelConfig.Items)
            {
                _itemQueue.Enqueue(item);
            }
        }

        // Заполнение активных объектов для поиска
        private void FillEmptySlots()
        {
            for (int i = 0; i < _activeItems.Length; i++)
            {
                if (_activeItems[i] == null && _itemQueue.Count > 0)
                {
                    var item = _itemQueue.Dequeue();
                    _activeItems[i] = item;
                    _signalBus.Fire(new ItemAssignedSignal
                    {
                        GetItemData = item,
                        SlotIndex = i
                    });
                }
            }
        }

        //Обработка объекта по которому кликнул пользователь на уровне
        private void OnItemClicked(ItemClickedSignal signal)
        {
            string clickedItemId = signal.ItemId;
            int slotIndex = FindItemSlot(clickedItemId);

            //Если объект по заданию в UI еще не должен быть найдет то получим -1 в FindItemSlot
            if (slotIndex != -1)
            {
                _activeItems[slotIndex] = null;
                _signalBus.Fire(new ItemFoundSignal
                {
                    ItemId = clickedItemId,
                    SlotIndex = slotIndex
                });

                //Двигаем очередь объектов
                if (_itemQueue.Count > 0)
                {
                    var nextItem = _itemQueue.Dequeue();
                    _activeItems[slotIndex] = nextItem;

                    _signalBus.Fire(new ItemAssignedSignal
                    {
                        GetItemData = nextItem,
                        SlotIndex = slotIndex
                    });
                }
            }
        }

        //Получение индекса слота объекта
        private int FindItemSlot(string itemId)
        {
            for (int i = 0; i < _activeItems.Length; i++)
            {
                if (!_activeItems[i]) continue;
                if (_activeItems[i].Id == itemId)
                    return i;
            }
            return -1;
        }

        //Получение ItemController среди объектов на уровне
        public ItemController GetItemController (string id)
        {
            foreach (var controller in LevelItems)
            {
                if (controller)
                {
                    if (controller.GetId() == id)
                        return controller;
                }
            }
            Debug.LogError("Итем не добавлен в LevelController");
            return null;
        }
    }
}