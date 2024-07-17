using TestTask.Factory;
using TestTask.MVVM;
using TestTask.Pools;
using UnityEngine;
using Zenject;
namespace TestTask.Infrastructure
{
    public class MainSceneInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private MainData _mainData;
        [SerializeField] private DefaultView _defaultView;
        [SerializeField] private AudioSource _effectsAudioSource;

        public override void InstallBindings()
        {
            BindSceneInstallerInterface();
            BindBulletFactory();
            BindBulletPool();
            BindEnemyFactory();
            BindEnemyPool();
            BindMainData();
            BindModel();
            BindDefaultViewModel();
            BindDefalutView();
            BindAudioSource();
        }

        private void BindAudioSource()
        {
            Container
                .Bind<AudioSource>()
                .FromInstance(_effectsAudioSource)
                .AsSingle()
                .NonLazy();
        }

        public void Initialize()
        {
            PrepareBulletPool();
            PrepareEnemyPool();
            Container.Resolve<EventBus>().OnGameActivityChanged += boolian => _mainData.GameIsActive = boolian;
            PrepareMVVM();
        }

        private void PrepareMVVM()
        {
            Model model = Container.Resolve<Model>();
            ViewModel viewModel = Container.Resolve<ViewModel>();
            View view = Container.Resolve<View>();

            view.Init(viewModel);
            viewModel.InitViewModel(model);
            model.Init(_mainData, Container.Resolve<EventBus>(), Container.Resolve<EnemyPool>());
        }

        private void BindDefalutView()
        {
            Container
                .Bind<View>()
                .To<DefaultView>()
                .FromInstance(_defaultView)
                .AsSingle()
                .NonLazy();
        }

        private void BindDefaultViewModel()
        {
            Container
                .Bind<ViewModel>()
                .To<DefaultViewModel>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindModel()
        {
            Container
                .Bind<Model>()
                .To<DefaultModel>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindMainData()
        {
            Container
                .Bind<MainData>()
                .FromInstance(_mainData)
                .AsSingle();
        }

        private void BindEnemyPool()
        {
            Container
                .Bind<EnemyPool>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IFactory>()
                .WithId(typeof(EnemyFactory))
                .To<EnemyFactory>()
                .FromNew()
                .AsTransient();
        }

        private void BindSceneInstallerInterface()
        {
            Container
                 .BindInterfacesTo<MainSceneInstaller>()
                 .FromInstance(this)
                 .AsSingle()
                 .NonLazy();
        }

        private void BindBulletPool()
        {
            Container
                .Bind<BulletPool>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindBulletFactory()
        {
            Container
                .Bind<IFactory>()
                .WithId(typeof(BulletFactory))
                .To<BulletFactory>()
                .FromNew()
                .AsTransient();
        }


        private void PrepareBulletPool()
        {
            IFactory bulletFactory = Container.TryResolveId<IFactory>(typeof(BulletFactory));
            bulletFactory.PrepareFactory(Container);
            Container.Resolve<BulletPool>().Init(bulletFactory, 15);

        }

        private void PrepareEnemyPool()
        {
            IFactory enemyFactory = Container.TryResolveId<IFactory>(typeof(EnemyFactory));
            enemyFactory.PrepareFactory(Container);
            Container.Resolve<EnemyPool>().Init(enemyFactory, 7);
        }
    } 
}
