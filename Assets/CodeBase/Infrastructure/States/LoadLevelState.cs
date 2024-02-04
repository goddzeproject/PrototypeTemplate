using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
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

            InitHud(hero);
            GameObject vCamera = InitVirtualCamera();
            CameraFollow(vCamera, arena);
        }

        private GameObject InitArena()
        {
            GameObject arena = _gameFactory.CreateArena(at: GameObject.FindWithTag(InitialPointTag));
            return arena;
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.ForLevel(sceneKey);
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
            }
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            hud.GetComponentInChildren<ActorUI>().
                Construct(hero.GetComponent<HeroHealth>());
        }

        private GameObject InitHero() => 
            _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));

        private GameObject InitVirtualCamera() => 
            _gameFactory.CreateVirtualCamera(GameObject.FindWithTag(InitialPointTag));

        private static void CameraFollow(GameObject vCamera, GameObject _object)
        {
            if (Camera.main != null) 
                Camera.main.GetComponent<CameraFollow>().FollowToObject(vCamera, _object);
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
    }
}