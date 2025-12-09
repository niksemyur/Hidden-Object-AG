using UnityEngine;
using Zenject;
using HiddenObject.Signals.GameState;

namespace HiddenObject.UI
{
    public class UI_ResultScreen : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;

        [Inject] private readonly SignalBus _signalBus;

        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<LevelCompletedSignal>(OnLevelComplete);
        }

        private void OnGameOver()
        {
            ShowLoseScreen();
        }

        private void OnLevelComplete()
        {
            ShowWinScreen();
        }

        private void ShowWinScreen() => winScreen.SetActive(true);
        private void ShowLoseScreen() => loseScreen.SetActive(true);

    }
}
