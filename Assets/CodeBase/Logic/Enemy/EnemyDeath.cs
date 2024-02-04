﻿using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public EnemyAnimator Animator;

        public GameObject DeathFx;

        public event Action Happened;

        private void Start()
        {
            Health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (Health.Current < 0)
                Die();
        }

        private void Die()
        {
            Health.HealthChanged -= HealthChanged;
            Animator.PlayDeath();
            
            var VFX = SpawnDeathFx();
            StartCoroutine(DestroyTimer(VFX));
            
            Happened?.Invoke();
        }

        private GameObject SpawnDeathFx()
        {
            GameObject VFX = Instantiate(DeathFx, transform.position, Quaternion.identity);
            return VFX;
        }

        private IEnumerator DestroyTimer(GameObject VFX)
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
            Destroy(VFX);
        }
    }
}