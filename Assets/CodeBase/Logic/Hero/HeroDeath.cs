using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Logic.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth Health;

        //public HeroMove Move;
        public CharacterMove Move;
        public HeroAttack Attack;
        public HeroAnimator Animator;
        public GameObject DeathFx;
        public bool isDead;

        private IWindowService _windowsService;

        private void Start() =>
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!isDead && Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            isDead = true;
            Move.enabled = false;
            Animator.PlayDeath();
            //Attack.enabled = false;
            Instantiate(DeathFx, transform.position, Quaternion.identity);
        }

        public void DestroyHero()
        {
            Destroy(gameObject);
        }
    }
}