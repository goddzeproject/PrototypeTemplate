using System.Collections;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public interface ILevelWatcher : IService
    {
        void StartWatching();
        void RegisterSpawner(SpawnPoint spawnPoint);
        void RegisterEnemy(GameObject enemy);
        void UnRegisterSpawner(SpawnPoint spawnPoint);
        void UnRegisterEnemy(GameObject enemy);
    }
}