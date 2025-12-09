using Zenject;
using HiddenObject.Managers;
using UnityEngine;

namespace HiddenObject.Installers 
{
    public class ManagersInstaller : MonoInstaller
    {
        [Header("Manager Prefabs")]

        [SerializeField] private InputManager _inputManagerPrefab;
        [SerializeField] private TimeManager _timeManagerPrefab;
        [SerializeField] private PauseManager _pauseManagerPrefab;
        [SerializeField] private GameManager _gameManagerPrefab;

        [Header("Parent")]
        [SerializeField] private Transform _managersParent;

        public override void InstallBindings()
        {
            BindManager<InputManager>(_inputManagerPrefab);
            BindManager<TimeManager>(_timeManagerPrefab);
            BindManager<PauseManager>(_pauseManagerPrefab);
            BindManager<GameManager>(_gameManagerPrefab);
        }

        private void BindManager<T>(T prefab) where T : MonoBehaviour
        {
            Container.BindInterfacesAndSelfTo<T>()
                .FromComponentInNewPrefab(prefab)
                .UnderTransform(_managersParent)
                .AsSingle()
                .NonLazy();
        }
    }
}
