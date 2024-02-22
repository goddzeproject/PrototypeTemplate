using UnityEngine;

namespace CodeBase.Logic.Enemy.BossBehaviour.Bullets
{
    public class DestroyBullet : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}