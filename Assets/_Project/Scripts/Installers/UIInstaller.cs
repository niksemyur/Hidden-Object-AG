using Zenject;
using HiddenObject.UI;

namespace HiddenObject.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UI_ItemSlotsManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UI_Timer>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UI_ResultScreen>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UI_PauseMenu>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
