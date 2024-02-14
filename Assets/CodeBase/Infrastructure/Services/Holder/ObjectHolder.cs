using System.Collections.Generic;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Holder
{
    class ObjectHolder : IObjectHolder
    {
        // public ObjectHolder(List<SpawnPoint> spawnPoints, List<GameObject> enemies)
        // {
        //     SpawnPoints = spawnPoints;
        //     Enemies = enemies;
        // }

        public GameObject HeroGameObject { get; set; }
        public List<SpawnPoint> SpawnPoints { get; set; } = new List<SpawnPoint>();
        public List<GameObject> Enemies { get; set; } = new List<GameObject>();

        public void RegisterHero(GameObject hero)
        {
        }

        public void RegisterSpawner(SpawnPoint spawnPoint)
        {
            SpawnPoints.Add(spawnPoint);
        }

        public void RegisterEnemy(GameObject enemy)
        {
            Enemies.Add(enemy);
        }

        public void UnRegisterSpawner(SpawnPoint spawnPoint)
        {
            if (SpawnPoints.Contains(spawnPoint))
                SpawnPoints.Remove(spawnPoint);
        }
        
    }
}