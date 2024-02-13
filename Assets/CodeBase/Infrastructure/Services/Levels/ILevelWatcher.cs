using System.Collections;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public interface ILevelWatcher : IService
    {
        GameObject HeroGameObject { get; set; }
        int ReturnCurrentLevel();
        void RestartLevel(int levelKey);
        void StartWatching();
        void ChangeLevel(int levelKey);
        void RegisterHero(GameObject hero);
        void RegisterSpawner(SpawnPoint spawnPoint);
        void RegisterEnemy(GameObject enemy);
        void UnRegisterSpawner(SpawnPoint spawnPoint);
        void UnRegisterEnemy(GameObject enemy);
    }
}