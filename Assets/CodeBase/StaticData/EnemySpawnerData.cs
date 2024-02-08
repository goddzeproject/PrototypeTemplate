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
        public Vector3 SpawnDirection;
        public int AmountEnemies;
        public float SpawnCooldown;
        public float FirstDelay;

        public EnemySpawnerData(string id, EnemyTypeId enemyTypeId, Vector3 position, Vector3 spawnDirection, int amountEnemies, float spawnCooldown, float firstDelay)
        {
            Id = id;
            this.enemyTypeId = enemyTypeId;
            Position = position;
            SpawnDirection = spawnDirection;
            AmountEnemies = amountEnemies;
            SpawnCooldown = spawnCooldown;
            FirstDelay = firstDelay;
        }
    }
}