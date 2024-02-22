using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Logic.Enemy.BossBehaviour.Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        public GameObject Wave;
        public GameObject Mine;

        public GameObject SpawnWave()
        {
            var wave = Instantiate(Wave, transform.position, quaternion.identity);
            return wave;
        }
        public GameObject SpawnMine(Vector3 point)
        {
            var mine = Instantiate(Mine, point, quaternion.identity);
            return mine;
        }
    }
}