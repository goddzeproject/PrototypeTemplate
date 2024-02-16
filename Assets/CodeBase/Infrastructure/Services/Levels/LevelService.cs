using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Holder;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Hero;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public class LevelService : ILevelService
    {
        private const string InitialPointTag = "InitialPoint";
        private const int InitialLevel = 1;

        private readonly IStaticDataService _staticDataService;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IObjectHolder _objectHolder;
        public GameObject HeroGameObject { set; get; }
        public GameObject Hud { set; get; }

        public LevelService(IGameFactory gameFactory, IStaticDataService staticDataService,
            IPersistentProgressService progressService, IObjectHolder objectHolder)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _progressService = progressService;
            _objectHolder = objectHolder;
        }


        public void InitLevelData()
        {
            InitSpawners(InitialLevel);
        }

        public GameObject InitHero()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
            HeroGameObject = hero;
            return HeroGameObject;
        }

        public void InitSpawners(int levelKey)
        {
            LevelStaticData levelData = _staticDataService.ForLevel(levelKey);

            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                SpawnPoint spawnPoint = _gameFactory.CreateSpawner(
                    spawnerData.Position, 
                    spawnerData.Id,
                    spawnerData.enemyTypeId,
                    spawnerData.SpawnDirection, 
                    spawnerData.AmountEnemies, 
                    spawnerData.SpawnCooldown, 
                    spawnerData.FirstDelay);
                
                _objectHolder.RegisterSpawner(spawnPoint);
            }
        }

        public void SpawnEnemies()
        {
            foreach (var spawnPoint in _objectHolder.SpawnPoints)
            {
                spawnPoint.Spawn();
            }
        }

        public GameObject InitArena() =>
            _gameFactory.CreateArena(at: GameObject.FindWithTag(InitialPointTag));

        public GameObject InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();
            hud.GetComponent<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
            Hud = hud;
            return Hud;
        }

        public GameObject InitVirtualCamera() =>
            _gameFactory.CreateVirtualCamera(GameObject.FindWithTag(InitialPointTag));

        public void CameraFollow(GameObject vCamera, GameObject _object)
        {
            if (Camera.main != null)
                Camera.main.GetComponent<CameraFollow>().FollowToObject(vCamera, _object);
        }

        public void UpdateHud()
        {
            //feture logic of update level count and etc...
        }

        public void InformProgressReaders()
        {
            _progressService.Progress.HeroState.ResetHP();
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        public void ClenUpProgressReaders()
        {
            //_gameFactory.CleanUp();
        }

        public void ClearHero()
        {
            if (HeroGameObject != null)
                HeroGameObject.GetComponent<HeroDeath>().DestroyHero();
        }

        public void ClearSpawners()
        {
            foreach (var spawnPoint in _objectHolder.SpawnPoints)
            {
                if (spawnPoint != null)
                    spawnPoint.DestroySpawner();
            }
            
            _objectHolder.SpawnPoints.Clear();
        }

        public void ClearEnemies()
        {
            foreach (var enemy in _objectHolder.Enemies)
            {
                if (enemy != null)
                    enemy.GetComponent<EnemyDeath>().Die();
            }

            _objectHolder.Enemies.Clear();
        }
    }
}