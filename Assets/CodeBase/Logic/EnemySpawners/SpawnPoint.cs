﻿using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Enemy;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        [FormerlySerializedAs("MonsterTypeId")] public EnemyTypeId enemyTypeId;
        public string Id { get; set; }
        public bool _slain;
        private EnemyDeath _enemyDeath;
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearSpawners.Contains(Id))
                _slain = true;
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject enemy = _gameFactory.CreateEnemy(enemyTypeId, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null) 
                _enemyDeath.Happened -= Slay;
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearSpawners.Add(Id);
        }
    }
}