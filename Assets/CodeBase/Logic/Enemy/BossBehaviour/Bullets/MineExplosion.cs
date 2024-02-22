using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Enemy.BossBehaviour.Bullets
{
    public class MineExplosion : MonoBehaviour
    {
        public DestroyBullet DestroyBullet;
        public float ExplodeTime = 5f;

        private void Start() => 
            StartCoroutine(ExplodeTimer());

        private IEnumerator ExplodeTimer()
        {
            yield return new WaitForSeconds(ExplodeTime);
            Explode();
        }

        private void Explode()
        {
            DestroyBullet.Destroy();
        }

    }
}