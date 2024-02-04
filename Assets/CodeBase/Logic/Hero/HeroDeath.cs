﻿using System.Collections;
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
        
        public WindowId WindowId;
        
        private bool _isDead;
        private IWindowService _windowsService;

        public void Construct(IWindowService windowsService)
        {
            _windowsService = windowsService;
        }

        private void Start() =>
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && Health.Current <= 0)
                Die();
        }

        private void Die()
        {

            _isDead = true;
            Move.enabled = false;
            Animator.PlayDeath();
            //Attack.enabled = false;

            Instantiate(DeathFx, transform.position, Quaternion.identity);
            
            StartCoroutine(StartTimerOpenRMenu());
        }

        private IEnumerator StartTimerOpenRMenu()
        {
            yield return new WaitForSeconds(2f);
            OpenRMenu();
        }

        private void OpenRMenu()
        {
            _windowsService.Open(WindowId);
        }
        
        
    }
}