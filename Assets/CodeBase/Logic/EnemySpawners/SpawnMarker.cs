using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        [FormerlySerializedAs("MonsterTypeId")] public EnemyTypeId enemyTypeId;
        [FormerlySerializedAs("SpawnPosition")] public Vector3 SpawnDirection;
        [FormerlySerializedAs("AmountEnemies")] public int unitsToSpawn;
        public float SpawnCooldown;
        public float FirstDelay;
    }
}