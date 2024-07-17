using TestTask.InputSystem;
using Zenject;

namespace TestTask.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {

            BindEventBus();
            BindStandartInputMap();
            BindInputRecevier();
            BindInputRecevierInterfaces();
        }

        private void BindInputRecevierInterfaces()
        {
            Container
                .BindInterfacesTo<InputReceiver>()
                .FromResolve();
        }

        private void BindInputRecevier()
        {
            Container
                .Bind<InputReceiver>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindStandartInputMap()
        {
            Container
                .Bind<StandartInputMap>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindEventBus()
        {
            Container
                .Bind<EventBus>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    } 
}
