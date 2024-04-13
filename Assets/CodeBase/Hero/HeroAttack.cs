using System;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Hero.Piano;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator HeroAnimator;
        private IInputService _input;
        
        private float radius;
        private Stats _stats;
        private PianoKeyLogic pianoKey;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            pianoKey = other.gameObject.GetComponent<PianoKeyLogic>();
        }


        private void Update() => 
            Play();

        private void Play()
        {
            if (_input.IsAtackButtonUp())
            {
                //HeroAnimator.PlayAttack();
                
            }
            if (_input.IsKeyDownPlay())
            {
                //HeroAnimator.PlayAttack();
                pianoKey.Play();
            }
        }


        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.HeroStats;
    }
}