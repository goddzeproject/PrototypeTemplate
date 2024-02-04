using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyHide : MonoBehaviour
    {
        public TriggerObserver _TriggerObserver;
        public float TimeDestroy = 3f;

        private void Start()
        {
            _TriggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerExit(Collider obj)
        {
            if(obj.CompareTag("Enemy"))
                StartCoroutine(DestroyObject());    
        }

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(TimeDestroy);

            Destroy(gameObject);
        }
    }
}