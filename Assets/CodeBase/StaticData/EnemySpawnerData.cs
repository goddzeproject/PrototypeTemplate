using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData
{
[Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        [FormerlySerializedAs("MonsterTypeId")] public EnemyTypeId enemyTypeId;
        public Vector3 Position;
        public Transform SpawnPosition;
        public int AmountEnemies;
        public float SpawnCooldown;

        public EnemySpawnerData(string id, EnemyTypeId enemyTypeId, Vector3 position, Transform spawnPosition, int amountEnemies, float spawnCooldown)
        {
            Id = id;
            this.enemyTypeId = enemyTypeId;
            Position = position;
            SpawnPosition = spawnPosition;
            AmountEnemies = amountEnemies;
            SpawnCooldown = spawnCooldown;
        }
    }
}