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

        public EnemySpawnerData(string id, EnemyTypeId enemyTypeId, Vector3 position)
        {
            Id = id;
            this.enemyTypeId = enemyTypeId;
            Position = position;
        }
    }
}