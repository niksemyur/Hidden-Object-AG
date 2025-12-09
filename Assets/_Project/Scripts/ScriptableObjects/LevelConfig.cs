using System.Collections.Generic;
using UnityEngine;
using HiddenObject.Managers;

namespace HiddenObject.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "_Core/Configs/Level/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Level Items")]
        [SerializeField] private List<ItemData> _items = new List<ItemData>();

        [Header("UI Settings")]
        [SerializeField] private int _maxfindingSlotsCount;
        [SerializeField] private bool _showImages = false;

        [Header("Timer Settings")]
        [SerializeField] private bool _useTimer = true;
        [SerializeField] private float _timerSeconds = 120f;

        [Header("Level Prefab")]
        [SerializeField] LevelController _levelController;

        public List<ItemData> Items => _items;
        public bool ShowImages => _showImages;
        public bool UseTimer => _useTimer;
        public float TimerSeconds => _timerSeconds;
        public int MaxFindingSlotsCount => _maxfindingSlotsCount;
        public LevelController GetLevelController () => _levelController;
    }
}