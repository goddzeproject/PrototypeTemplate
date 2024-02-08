using System;
using System.Collections;
using CodeBase.Infrastructure.Services.Levels;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyGone : MonoBehaviour
    {
        public TriggerObserver _TriggerObserver;
        public bool isGone;

        private void Start()
        {
            _TriggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerExit(Collider obj)
        {
            if (obj.CompareTag("Enemy"))
                isGone = true;
        }
    }
}