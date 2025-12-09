using UnityEngine;
using Zenject;
using HiddenObject.Configs;
using HiddenObject.Managers;

namespace HiddenObject.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelConfig _levelConfig;

        public override void InstallBindings()
        {
            Container.Bind<LevelConfig>()
                .FromInstance(_levelConfig)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<LevelController>()
                .FromComponentInNewPrefab(_levelConfig.GetLevelController())
                .UnderTransform(transform)
                .AsSingle()
                .NonLazy();
        }
    }
}