using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [FormerlySerializedAs("EnemyTypeId")] public EnemyTypeId enemyTypeId;
        
        [Range(1, 100)]
        public int Hp;
        
        [Range(1, 30)]
        public float Damage;

        public int MinLoot;
        public int MaxLoot;
        
        
        [Range(0.5f, 1)]
        public float EffectiveDistance = 0.666f;
        
        [Range(0.5f, 1)]
        public float Cleavage;

        [Range(1f, 10)]
        public float MoveSpeed;
        
        public GameObject Prefab;
    }
}