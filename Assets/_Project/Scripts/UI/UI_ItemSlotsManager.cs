using UnityEngine;
using Zenject;
using HiddenObject.Configs;
using HiddenObject.Signals.Item;

namespace HiddenObject.UI
{
    public class UI_ItemSlotsManager : MonoBehaviour, IInitializable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LevelConfig _levelConfig;

        [SerializeField] private Transform slotContainer;
        [SerializeField] private UI_ItemSlot itemSlot_Prefab;

        private UI_ItemSlot[] _itemSlots;

        public void Initialize()
        {
            _signalBus.Subscribe<ItemAssignedSignal>(OnItemAssigned);
            _signalBus.Subscribe<ItemFoundSignal>(OnItemFound);

            _itemSlots = new UI_ItemSlot[_levelConfig.MaxFindingSlotsCount];
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i] = Instantiate(itemSlot_Prefab, slotContainer);
                _itemSlots[i].ClearSlot();
            }
        }

        //Дисплей итема для поиска в слоте UI
        private void OnItemAssigned(ItemAssignedSignal signal)
        {
            var slot = _itemSlots[signal.SlotIndex];

            if (_levelConfig.ShowImages)
                slot.LoadSlot(signal.GetItemData.UiSprite);
            else
                slot.LoadSlot(signal.GetItemData.DisplayName);
        }

        //Очищение слота
        private void OnItemFound(ItemFoundSignal signal)
        {
            _itemSlots[signal.SlotIndex].ClearSlot();
        }
    }
}
