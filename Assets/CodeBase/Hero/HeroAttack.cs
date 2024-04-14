using System;
using CodeBase.Data;
using CodeBase.Hero.Piano;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private IInputService _input;
        private AnimationManager _animationManager;
        private HeroMove _heroMove;
        private Animator _animator;

        private Vector3 standingScale;
        private Vector3 squattingScale;

        private bool isSquatting = false;

        private float radius;
        private Stats _stats;

        private float cooldownTime = 0.1f;
        private bool isColdown = false;

        private void Awake() => 
            _input = AllServices.Container.Single<IInputService>();

        private void Start()
        {
            _animationManager = AnimationManager.Instance;
            _heroMove = GetComponent<HeroMove>();
            standingScale = transform.localScale;
            squattingScale = new Vector3(standingScale.x, 0.5f, standingScale.z);
            _animator = GetComponent<Animator>();
        }


        private void Update() => 
            Play();

        private void Play()
        {
            if (_input.IsAtackButtonUp())
            {
                
            }
            if ((_input.IsKeyDownPlaySpace() || _input.IsKeyDownPlayEnter()) && !isColdown)
            {
                _animator.SetTrigger("Release");


                Debug.Log(_heroMove.currentPos);
                Squat(squattingScale); // animation 

                _animationManager.PlayAnimKey(_heroMove.currentPos);
                //ChangeColor();

                isColdown = true;
                Invoke("CooldownComplete", cooldownTime);
            }
        }
        private void Squat(Vector3 squattingScale)
        {
            
            if (isSquatting) return;
            isSquatting = true;
            
            // if (_animationManager.IsAnimationPlaying) return;
            // _animationManager.StartAnimation();
            
            Sequence squatSequence = DOTween.Sequence();
            squatSequence.Append(transform.DOScale(squattingScale, 0.05f).SetEase(Ease.InOutQuad));
            squatSequence.Append(transform.DOScale(standingScale, 0.05f).SetEase(Ease.InOutQuad));
            squatSequence.OnComplete(() => isSquatting = false);
            
            // squatSequence.OnComplete(() => _animationManager.StopAnimation());
            
        }

        // private void ChangeColor()
        // {
        //     Color randomColor = new Color(Random.value, Random.value, Random.value);
        //     // Устанавливаем этот случайный цвет в качестве цвета фона камеры
        //     _camera.backgroundColor = randomColor;
        // }

        private void  CooldownComplete() => 
            isColdown = false;


        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.HeroStats;
    }
}