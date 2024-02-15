using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Holder
{
    public interface IObjectHolder : IService
    {
        GameObject HeroGameObject { get; set; }
        List<SpawnPoint> SpawnPoints { get; set; }

        List<GameObject> Enemies { get; set; }
        void RegisterHero(GameObject hero);
        void RegisterSpawner(SpawnPoint spawnPoint);
        void RegisterEnemy(GameObject enemy);
    }
}
