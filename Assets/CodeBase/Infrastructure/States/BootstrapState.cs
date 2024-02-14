using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Holder;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Levels;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SceneLoader _sceneLoader;

        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(name: Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();
            
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IPersistentProgressService>(new  PersistentProgressService());
            _services.RegisterSingle<ICoroutineRunner>(_coroutineRunner);
            _services.RegisterSingle<IObjectHolder>( new ObjectHolder());


            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssets>(), _services.Single<IStaticDataService>(), _services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssets>(), 
                _services.Single<IStaticDataService>(),
                _services.Single<IRandomService>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<IWindowService>(), 
                _services.Single<IObjectHolder>()));
            

            _services.RegisterSingle<ILevelService>(new LevelService(
                _services.Single<IGameFactory>(),
                _services.Single<IStaticDataService>(), 
                _services.Single<IPersistentProgressService>(), 
                _services.Single<IObjectHolder>()));
            
            _services.RegisterSingle<ILevelWatcher>(new LevelWatcher(
                _services.Single<ILevelService>(), 
                _services.Single<IStaticDataService>(), 
                _services.Single<IWindowService>(),
                _services.Single<ICoroutineRunner>(),
                _services.Single<IObjectHolder>()));


            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), 
                _services.Single<IGameFactory>()));
        }
        
        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadDataResources();
            _services.RegisterSingle(staticData);
        }

        private static InputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputSevice();
            else
                return new MobileInputService();
        }
    }
}