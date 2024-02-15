using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Holder;
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

        public Vector3 SpawnDirection; // где он должен их отспавнить
        public int UnitsToSpawn; // количество сколько должен отспавнить мобов этот спавнер
        public float SpawnCooldown; // с каким кулдауном он будеи их спавнить
        public float FirstDelay;

        private EnemyDeath _enemyDeath;
        private IGameFactory _gameFactory;
        private IObjectHolder _objectHolder;
        private bool _isFirstSpawn;

        public void Construct(IGameFactory gameFactory, IObjectHolder objectHolder)
        {
            _gameFactory = gameFactory;
            _objectHolder = objectHolder;
        }


        public void Spawn()
        {
            StartCoroutine(SpawnUnits());
        }

        IEnumerator SpawnUnits()
        {
            while (UnitsToSpawn > 0)
            {
                if (!_isFirstSpawn)
                {
                    yield return new WaitForSeconds(FirstDelay);
                    _isFirstSpawn = true;
                }
                
                GameObject enemy = _gameFactory.CreateEnemy(enemyTypeId, transform, SpawnDirection);
                _objectHolder.RegisterEnemy(enemy);
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
            //_objectHolder.UnRegisterSpawner(this);
            Destroy(gameObject);
        }
    }
}