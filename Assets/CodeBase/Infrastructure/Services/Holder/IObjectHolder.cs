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
        List<GameObject> Plates { get; set; }
        List<GameObject> Waves { get; set; }
        List<GameObject> Mines { get; set; }
        void RegisterHero(GameObject hero);
        void RegisterSpawner(SpawnPoint spawnPoint);
        void RegisterEnemy(GameObject enemy);
        void RegisterPlate(GameObject plate);
        void RegisterWave(GameObject wave);
        void RegisterMine(GameObject mine);
    }
}
