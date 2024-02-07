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
        private readonly ILevelService _levelService;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;
        private readonly ICoroutineRunner _coroutineRunner;

        private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
        private List<GameObject> _enemies = new List<GameObject>();

        private List<int> levelKey = new List<int>();

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
        }

        private IEnumerator WatchUpdate()
        {
            while (true)
            {
                Debug.Log("Update");
                
                //WatchHero();
                //WatchLevel();

                yield return new WaitForSeconds(1f);
            }
        }


        private void WatchHero()
        {
            bool isDead = _levelService.HeroGameObject.GetComponent<HeroDeath>().isDead;
            if (isDead)
                _coroutineRunner.StartCoroutine(StartTimerOpenRMenu());
        }

        private void WatchLevel()
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints) //проверка логики смены уровня
            {
                //if (spawnPoint.UnitsToSpawn == 0) ;

            }
            
            if (true /*level Ended*/)
            {
                ChangeLevel(2);
            }
        }

        private void ChangeLevel(int levelKey)
        {
            ClearAndUnregisterEnemies();
            
            _levelService.CleanUpLevelData();
            _levelService.InitHero();
            _levelService.InitSpawners(levelKey);
            _levelService.SpawnEnemies();
            
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
                UnRegisterEnemy(enemy);
            }
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