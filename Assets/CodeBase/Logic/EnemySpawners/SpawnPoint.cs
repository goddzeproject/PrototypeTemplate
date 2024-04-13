using System;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.ObjectHolder;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour
    {
        public MonsterTypeId MonsterTypeId;
        public string Id { get; set; }
        public bool _slain;
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void Start() => 
            AllServices.Container.Single<ISpawnersHolderService>().RegisterObject(this);

        public void Spawn()
        {
            GameObject monster = _gameFactory.CreateMonster(MonsterTypeId, transform);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearSpawners.Add(Id);
        }
    }
}