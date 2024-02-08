using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Hero;
using CodeBase.StaticData;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CodeBase.Infrastructure.Services.Levels
{
    public class LevelWatcher : ILevelWatcher
    {
        private const string InitialPointTag = "InitialPoint";
        private ILevelService _levelService;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;
        private readonly ICoroutineRunner _coroutineRunner;

        private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
        private List<GameObject> _enemies = new List<GameObject>();

        private int _levelKey = 2;

        public GameObject HeroGameObject;

        private bool _isWindowOpen;

        public LevelWatcher(ILevelService levelService, IStaticDataService staticData, IWindowService windowService,
            ICoroutineRunner coroutineRunner)
        {
            _levelService = levelService;
            _staticData = staticData;
            _windowService = windowService;
            _coroutineRunner = coroutineRunner;
        }

        public void StartWatching()
        {
            _coroutineRunner.StartCoroutine(WatchUpdate());
            _levelService = AllServices.Container.Single<ILevelService>(); // Это пока Костыль!!!
        }

        private IEnumerator WatchUpdate()
        {
            while (true)
            {
                Debug.Log("Update");

                if (!WatchHero() & WatchSpawners() & WatchEnemies())
                    ChangeLevel(_levelKey++);

                yield return new WaitForSeconds(1f);
            }
        }


        public void RegisterHero(GameObject hero) =>
            HeroGameObject = hero;

        private void ChangeLevel(int levelKey) // Смена уровня - подчистка старого уровня и вызов создания нового
        {
            Debug.Log(levelKey);
            
            ClearAndUnregisterEnemies();
            _levelService.ClearEnemies();
            _levelService.ClearSpawners();

            if (WatchHero())
            {
                _levelService.ClearHero();
                _levelService.InitHero();
            }

            _levelService.InitSpawners(levelKey);
            
            _levelService.SpawnEnemies();

            _isWindowOpen = false;
        }

        private bool WatchHero() // проверка живой ли герой и вызов Окна при смерти
        {
            bool isDead = HeroGameObject.GetComponent<HeroDeath>().isDead;
            if (isDead && !_isWindowOpen)
            {
                _coroutineRunner.StartCoroutine(StartTimerOpenRMenu());
                _isWindowOpen = true;
            }

            return isDead;
        }

        private bool WatchSpawners() // проверка за спавнерами
        {
            return _spawnPoints.Count != 0 && _spawnPoints.All(spawnPoint => spawnPoint.UnitsToSpawn == 0);
        }

        private bool WatchEnemies() // проверка врагов
        {
            return _enemies.All(enemies => enemies.GetComponent<EnemyGone>().isGone);
        }

        private IEnumerator StartTimerOpenRMenu()
        {
            yield return new WaitForSeconds(2f);
            OpenRMenu();
        }

        private void OpenRMenu() =>
            _windowService.Open(WindowId.RMenu);

        private void ClearAndUnregisterEnemies()
        {
            
            foreach (var enemy in _enemies)
            {
                enemy.GetComponent<EnemyDeath>().Die();
            }

            _enemies.Clear();
        }


        public void RegisterSpawner(SpawnPoint spawnPoint) =>
            _spawnPoints.Add(spawnPoint);

        public void UnRegisterSpawner(SpawnPoint spawnPoint)
        {
            if (_spawnPoints.Contains(spawnPoint))
                _spawnPoints.Remove(spawnPoint);
        }

        public void RegisterEnemy(GameObject enemy) =>
            _enemies.Add(enemy);

        public void UnRegisterEnemy(GameObject enemy)
        {
            if (_enemies.Contains(enemy))
                _enemies.Remove(enemy);
        }
    }
}