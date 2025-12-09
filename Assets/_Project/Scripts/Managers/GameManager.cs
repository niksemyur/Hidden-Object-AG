using UnityEngine;
using Zenject;
using HiddenObject.Configs;
using HiddenObject.Signals.Item;
using HiddenObject.Signals.Time;
using HiddenObject.Signals.GameState;

namespace HiddenObject.Managers
{
    public class GameManager : MonoBehaviour, IInitializable
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LevelConfig _levelConfig;
        [Inject] private readonly LevelController _levelController;

        private int _foundItemsCount = 0;
        private int _totalItemsToFind = 0;

        public void Initialize()
        {
            _signalBus.Subscribe<ItemFoundSignal>(OnItemFound);
            _signalBus.Subscribe<TimeOutSignal>(OnTimeOut);
            InitializeLevel();
        }

        private void InitializeLevel()
        {
            _foundItemsCount = 0;
            _totalItemsToFind = _levelConfig.Items.Count;
            _signalBus.Fire(new LevelStartedSignal { });
        }

        private void OnItemFound(ItemFoundSignal signal)
        {
            var clickedItem = _levelController.GetItemController(signal.ItemId);
            clickedItem.FindItem();

            _foundItemsCount++;
            CheckVictory();
        }

        private void CheckVictory()
        {
            if (_foundItemsCount >= _totalItemsToFind)
                Victory();
        }

        private void OnTimeOut()
        {
            Lose();
        }

        private void Lose()
        {
            _signalBus.Fire(new GameOverSignal { });
        }

        private void Victory()
        {
            _signalBus.Fire(new LevelCompletedSignal { });
        }
    }
}