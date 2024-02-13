using System;
using CodeBase.Logic.Enemy;
using UnityEngine;

namespace CodeBase.Logic.Hero
{
    public class HeroGone : MonoBehaviour
    {
        public bool isGone;

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Finish"))               
                isGone = true; 
        }
    }
}