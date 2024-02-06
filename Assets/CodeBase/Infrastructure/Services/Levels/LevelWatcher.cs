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
    public class LevelWatcher : ILevelWatcher, ICoroutineRunner
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly ILevelService _levelService;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;
        
        private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

        public LevelWatcher(ILevelService levelService, IStaticDataService staticData, IWindowService windowService)
        {
            _levelService = levelService;
            _staticData = staticData;
            _windowService = windowService;
        }
        

        private IEnumerable Update()
        {
            //Cycle update with coroutine;
            WatchHero();
            WatchRound();
            
            yield return new WaitForSeconds(1f);
            

        }

        private void WatchRound()
        {
            if (true/*round Ended*/)
                ChangeRound( 2);
        }


        public void ChangeRound(int levelKey)
        {
            _levelService.CleanUpLevelData();
            _levelService.InitHero();
            _levelService.InitSpawners(levelKey);
        }

        private void WatchHero()
        {
            bool isDead = _levelService.HeroGameObject.GetComponent<HeroDeath>().isDead;
            if (isDead) 
                StartCoroutine(StartTimerOpenRMenu());
        }

        private IEnumerator StartTimerOpenRMenu()
        {
            yield return new WaitForSeconds(2f);
            OpenRMenu();
        }

        private void OpenRMenu()
        {
            _windowService.Open(WindowId.RMenu);
        }


        public void Register(SpawnPoint spawnPoint)
        {
            _spawnPoints.Add(spawnPoint);
        }

        public void UnRegister(SpawnPoint spawnPoint)
        {
            if (_spawnPoints.Contains(spawnPoint)) 
                _spawnPoints.Remove(spawnPoint);
        }


        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            Coroutine startCoroutine = StartCoroutine(coroutine);
            return startCoroutine;
        }
    }
}