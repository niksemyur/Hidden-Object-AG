using UnityEngine;
using Zenject;
using HiddenObject.Configs;
using HiddenObject.Signals.GameState;
using HiddenObject.Signals.Time;

namespace HiddenObject.Managers
{
    public class TimeManager : MonoBehaviour, IInitializable, ITickable
    {
        [SerializeField] private float eventSendInterval = 1f; //интервал отправки события апдейта времени, равен секунде

        [Inject] private readonly LevelConfig _levelConfig;
        [Inject] private readonly SignalBus _signalBus;

        private float _timeLeft;
        private float _eventCooldownTimer;

        private bool _isTimerRunning;
        private bool _gamePause;

        public void Initialize()
        {
            _timeLeft = _levelConfig.TimerSeconds;
            _eventCooldownTimer = eventSendInterval;

            _signalBus.Subscribe<LevelStartedSignal>(OnLevelStarted);
            _signalBus.Subscribe<LevelCompletedSignal>(OnLevelCompleted);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);

            _signalBus.Subscribe<GamePauseSignal>(OnPause);
            _signalBus.Subscribe<GameResumeSignal>(OnResume);
        }
        private void OnLevelStarted()
        {
            StartTimer();
        }
        private void OnLevelCompleted()
        {
            StopTimer();
        }
        private void OnGameOver()
        {
            StopTimer();
        }
        public void Tick()
        {
            if (!_isTimerRunning) return;
            if (_gamePause) return;

            float deltaTime = Time.deltaTime;
            _timeLeft -= deltaTime;
            _eventCooldownTimer += deltaTime;

            if (_timeLeft <= 0f)
            {
                _eventCooldownTimer = eventSendInterval;
                _timeLeft = 0;
                OnTimerEnded();
            }

            if (_eventCooldownTimer >= eventSendInterval)
            {
                _eventCooldownTimer = 0;
                _signalBus.Fire(new TimeUpdatedSignal
                {
                    TimeLeft = _timeLeft
                });
            }
        }

        private void StartTimer()
        {
            _isTimerRunning = true;
        }

        private void StopTimer()
        {
            _isTimerRunning = false;
        }

        private void OnTimerEnded()
        {
            _signalBus.Fire(new TimeOutSignal());
        }

        private void OnPause()
        {
            _gamePause = true;
        }
        private void OnResume()
        {
            _gamePause = false;
        }
    }
}