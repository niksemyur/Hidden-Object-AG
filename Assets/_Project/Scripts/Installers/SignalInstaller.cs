using Zenject;
using HiddenObject.Signals.Time;
using HiddenObject.Signals.Item;
using HiddenObject.Signals.GameState;
using HiddenObject.Signals.Input;

namespace HiddenObject.Installers
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ItemClickedSignal>();
            Container.DeclareSignal<ItemAssignedSignal>();
            Container.DeclareSignal<ItemFoundSignal>();

            Container.DeclareSignal<LevelStartedSignal>();
            Container.DeclareSignal<LevelCompletedSignal>();
            Container.DeclareSignal<GameOverSignal>();

            Container.DeclareSignal<TimeUpdatedSignal>();
            Container.DeclareSignal<TimeOutSignal>();

            Container.DeclareSignal<GamePauseSignal>();
            Container.DeclareSignal<GameResumeSignal>();

            Container.DeclareSignal<EscapeKeyPressedSignal>();
        }
    }
}