using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Levels;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Enemy;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        [FormerlySerializedAs("MonsterTypeId")]
        public EnemyTypeId enemyTypeId;

        public string Id { get; set; }
        public bool _slain;

        public Transform SpawnPosition; // где он должен их отспавнить
        [FormerlySerializedAs("unitsToSpawn")] public int UnitsToSpawn; // количество сколько должен отспавнить мобов этот спавнер
        public float SpawnCooldown; // с каким кулдауном он будеи их спавнить

        private EnemyDeath _enemyDeath;
        private IGameFactory _gameFactory;
        private ILevelWatcher _levelWatcher;

        public void Construct(IGameFactory gameFactory, ILevelWatcher iLevelWatcher)
        {
            _gameFactory = gameFactory;
            _levelWatcher = iLevelWatcher;
        }

        private void Start()
        {
            _levelWatcher.RegisterSpawner(this);
        }


        public void Spawn()
        {
            StartCoroutine(SpawnUnits());
        }

        IEnumerator SpawnUnits()
        {
            while (UnitsToSpawn > 0)
            {
                GameObject enemy = _gameFactory.CreateEnemy(enemyTypeId, transform);
                _levelWatcher.RegisterEnemy(enemy);
                UnitsToSpawn--;
                yield return new WaitForSeconds(SpawnCooldown);
            }
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Slay;
            _slain = true;
        }


        public void DestroySpawner()
        {
            _levelWatcher.UnRegisterSpawner(this);
            Destroy(gameObject);
        }
    }
}