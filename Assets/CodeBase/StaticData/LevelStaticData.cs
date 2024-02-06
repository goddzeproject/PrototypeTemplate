using System.Collections.Generic;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public int LevelKey;
        public List<EnemySpawnerData> EnemySpawners;
    }
}