using UnityEngine;
using Zenject;
using HiddenObject.Signals.GameState;

namespace HiddenObject.UI
{
    public class UI_PauseMenu : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject pauseMenu;

        [Inject] private readonly SignalBus _signalBus;

        public void Initialize()
        {
            _signalBus.Subscribe<GamePauseSignal>(OnPaused);
            _signalBus.Subscribe<GameResumeSignal>(OnResume);
        }

        private void OnPaused()
        {
            pauseMenu.SetActive(true);
        }

        private void OnResume()
        {
            pauseMenu.SetActive(false);
        }
    }
}
