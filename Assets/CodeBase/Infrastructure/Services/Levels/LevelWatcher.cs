using System.Collections;
using System.Linq;
using CodeBase.Infrastructure.Services.Holder;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.Hero;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public class LevelWatcher : ILevelWatcher
    {
        private ILevelService _levelService;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IObjectHolder _objectHolder;

        public int LevelKey = 1;
        

        private bool _isWindowOpen;


        public LevelWatcher(ILevelService levelService, IStaticDataService staticData, IWindowService windowService,
            ICoroutineRunner coroutineRunner, IObjectHolder objectHolder)
        {
            _levelService = levelService;
            _staticData = staticData;
            _windowService = windowService;
            _coroutineRunner = coroutineRunner;
            _objectHolder = objectHolder;
        }

        public void StartWatching()
        {
            _coroutineRunner.StartCoroutine(WatchUpdate());
        }

        public int ReturnCurrentLevel() => 
            LevelKey;

        public void RestartLevel(int levelKey)
        {
            LevelKey = levelKey;
        }

        private IEnumerator WatchUpdate()
        {
            while (true)
            {
                Debug.Log("Update");

                if (!WatchHero() & WatchSpawners() & WatchEnemies())
                    ChangeLevel(++LevelKey);

                yield return new WaitForSeconds(1f);
            }
        }

        

        public void ChangeLevel(int levelKey) // Смена уровня - подчистка старого уровня и вызов создания нового
        {
            Debug.Log(levelKey);
            
            _levelService.ClearEnemies();
            _levelService.ClearSpawners();

            if (WatchHero())
            {
                _levelService.ClearHero();
                GameObject hero = _levelService.InitHero();
                //_levelService.InitHud(hero);
            }

            _levelService.InitSpawners(levelKey);
            _levelService.SpawnEnemies();

            _isWindowOpen = false;
            
            _levelService.InformProgressReaders();
        }

        private bool WatchHero() // проверка живой ли герой и вызов Окна при смерти
        {
            bool isDead = _levelService.HeroGameObject.GetComponent<HeroDeath>().isDead;

            if (_levelService.HeroGameObject.GetComponent<HeroGone>().isGone)
                isDead = true;
            
            if (isDead && !_isWindowOpen)
            {
                _coroutineRunner.StartCoroutine(StartTimerOpenRMenu());
                _isWindowOpen = true;
            }

            return isDead;
        }

        private bool WatchSpawners() // проверка за спавнерами
        {
            return _objectHolder.SpawnPoints.Count != 0 && _objectHolder.SpawnPoints.All(spawnPoint => spawnPoint.UnitsToSpawn == 0);
        }

        private bool WatchEnemies() // проверка врагов
        {
            return _objectHolder.Enemies.All(enemies => enemies.GetComponent<EnemyGone>().isGone);
        }

        private IEnumerator StartTimerOpenRMenu()
        {
            yield return new WaitForSeconds(1f);
            OpenRMenu();
        }
        

        private void OpenRMenu() =>
            _windowService.Open(WindowId.RestartMenu);
    }
}