using UnityEngine;
using Zenject;
using HiddenObject.Signals.GameState;
using HiddenObject.Signals.Input;

namespace HiddenObject.Managers
{
    public class PauseManager : MonoBehaviour, IInitializable
    {
        [Inject]
        private SignalBus _signalBus;
        private bool IsPause;

        public void Initialize()
        {
            _signalBus.Subscribe<EscapeKeyPressedSignal>(TogglePause);
        }

        private void TogglePause()
        {
            IsPause = !IsPause;
            if (IsPause)
                _signalBus.Fire<GamePauseSignal>();
            else
                _signalBus.Fire<GameResumeSignal>();
        }
    }
}
