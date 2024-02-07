using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        [FormerlySerializedAs("MonsterTypeId")] public EnemyTypeId enemyTypeId;
        public Transform SpawnPosition;
        [FormerlySerializedAs("AmountEnemies")] public int unitsToSpawn;
        public float SpawnCooldown;
    }
}