using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Hero;
using CodeBase.Logic.Loot;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _proggressService;
        private readonly IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }
        private GameObject VirtualCamera { get; set; }

        public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService random,
            IPersistentProgressService persistentProgressService, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = random;
            _proggressService = persistentProgressService;
            _windowService = windowService;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateVirtualCamera(GameObject at)
        {
            GameObject virtualCamera = InstantiateRegistered(AssetPath.VirtualCamera, at.transform.position);
            VirtualCamera = virtualCamera;
            return VirtualCamera;
        }

        public GameObject CreateArena(GameObject at)
        {
            GameObject arena = InstantiateRegistered(AssetPath.Arena, at.transform.position);
            return arena;
        }

        public GameObject CreateHero(GameObject at)
        {
            GameObject hero = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
            HeroGameObject = hero;
            HeroGameObject.GetComponent<HeroDeath>().Construct(_windowService);
            return HeroGameObject;
        }

        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();

            lootPiece.Construct(_proggressService.Progress.WorldData);
            return lootPiece;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_proggressService.Progress.WorldData);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);

            return hud;
        }

        public void CreateSpawner(Vector3 at, string spawnerId, EnemyTypeId enemyTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at).GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.enemyTypeId = enemyTypeId;
        }

        public GameObject CreateEnemy(EnemyTypeId typeId, Transform parent)
        {
            EnemyStaticData enemyData = _staticData.ForMonster(typeId);
            GameObject enemy = Object.Instantiate(enemyData.Prefab, parent.position, Quaternion.identity, parent);


            var health = enemy.GetComponent<IHealth>();
            health.Current = enemyData.Hp;
            health.Max = enemyData.Hp;

            enemy.GetComponent<ActorUI>().Construct(health);
            enemy.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);

            enemy.GetComponent<NavMeshAgent>().speed = enemyData.MoveSpeed;

            // var lootSpawners = monster.GetComponentInChildren<LootSpawner>();
            // lootSpawners.Construct(this, _randomService);
            // lootSpawners.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            // var attack = monster.GetComponent<Attack>();
            // attack.Construct(HeroGameObject.transform);
            // attack.Damage = monsterData.Damage;
            // attack.Cleavage = monsterData.Cleavage;
            // attack.EffectiveDistance = monsterData.EffectiveDistance;

            enemy.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);
            
            enemy.GetComponent<SayFuckToHero>()?.Construct(HeroGameObject.transform);

            return enemy;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Initialize(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Initialize(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}