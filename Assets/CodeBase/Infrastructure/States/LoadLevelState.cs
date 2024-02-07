using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Levels;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.Hero;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;
        private readonly ILevelService _levelService;
        private readonly ILevelWatcher _levelWatcher;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory, ILevelService levelService, ILevelWatcher levelWatcher)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _levelService = levelService;
            _levelWatcher = levelWatcher;
        }

        public void Enter(string sceneName)
        {   
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLoaded()
        {
            InitUIRoot();
            
            InitGameWorld();
            
            InformProgressReaders();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() => 
            _uiFactory.CreateUIRoot();

        private void InitGameWorld()
        {
            InitSpawners();

            GameObject arena = InitArena();
            GameObject hero = InitHero();
            
            SpawnEnemy();

            InitHud(hero);
            GameObject vCamera = InitVirtualCamera();
            CameraFollow(vCamera, arena);

            _levelWatcher.StartWatching();
        }

        private void SpawnEnemy()
        {
            _levelService.SpawnEnemies();
        }

        private GameObject InitArena() => 
            _levelService.InitArena();

        private void InitSpawners() => 
            _levelService.InitLevelData();

        private GameObject InitHero() => 
            _levelService.InitHero();

        private void InitHud(GameObject hero) => 
            _levelService.InitHud(hero);

        private GameObject InitVirtualCamera() => 
            _levelService.InitVirtualCamera();

        private void CameraFollow(GameObject vCamera, GameObject _object) => 
            _levelService.CameraFollow(vCamera, _object);

        private void InformProgressReaders() => 
            _levelService.InformProgressReaders();
    }
}