using TMPro;
using UnityEngine;
using Zenject;
using HiddenObject.Signals.Time;

namespace HiddenObject.UI
{
    public class UI_Timer : MonoBehaviour, IInitializable
    {
        [SerializeField] private TextMeshProUGUI TimerTxt;

        [Inject] private readonly SignalBus _signalBus;

        public void Initialize()
        {
            _signalBus.Subscribe<TimeUpdatedSignal>(OnTimeUpdated);
        }

        private void OnTimeUpdated(TimeUpdatedSignal signal)
        {
            TimerTxt.text = GetFormattedTime(signal.TimeLeft);
        }

        private string GetFormattedTime(float timeLeft)
        {
            // Используем CeilToInt чтобы 0.9 секунды отображалось как 1 секунда
            int displaySeconds = Mathf.CeilToInt(Mathf.Max(0, timeLeft));
            int minutes = displaySeconds / 60;
            int seconds = displaySeconds % 60;

            return $"{minutes:00}:{seconds:00}";
        }
    }
}
