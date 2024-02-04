using System.Linq;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator Animator;
    
        public float AttackCoolDown = 3f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 0.5f;
        public float Damage = 10f;
        
        private Transform _heroTransform;
        private float _attackCoolDown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _attackCoolDown = AttackCoolDown;
            _isAttacking = false;
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            var hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitAmount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward*EffectiveDistance;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCoolDown -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            Animator.PlayAttack();

            _isAttacking = true;
        }

        private bool CanAttack() => 
            _attackIsActive&& !_isAttacking && CooldownIsUp();

        private bool CooldownIsUp() => 
            _attackCoolDown <= 0f;
    }
}