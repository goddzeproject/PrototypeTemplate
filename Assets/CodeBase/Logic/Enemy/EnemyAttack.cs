using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 30f;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            if (obj.CompareTag("Player"))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                obj.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void TriggerExit(Collider obj)
        {
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                   transform.forward * EffectiveDistance;
        }
    }
}