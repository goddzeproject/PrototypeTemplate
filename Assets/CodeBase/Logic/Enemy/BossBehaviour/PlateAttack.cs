using System;
using UnityEngine;

namespace CodeBase.Logic.Enemy.BossBehaviour
{
    public class PlateAttack : MonoBehaviour
    {
        public float Damage = 20f;
        public TriggerObserver TriggerObserver;
        [Space] public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;

        private IHealth _bossHealth;

        public void Construct(GameObject boss)
        {
            _bossHealth = boss.GetComponent<IHealth>();
        }

        private void Start() => 
            TriggerObserver.TriggerEnter += TriggerEnter;

        private void OnDestroy() => 
            TriggerObserver.TriggerEnter -= TriggerEnter;

        private void TriggerEnter(Collider obj)
        {
            if (obj.CompareTag("Player"))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                _bossHealth.TakeDamage(Damage);
                Destroy();
            }
        }

        public void Destroy() =>
            Destroy(gameObject);


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