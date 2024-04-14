using CodeBase.Data;
using CodeBase.Hero.Piano;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public Transform[] Points;
        public int currentPos = 0;
        
        private float cooldownTime = 0.1f;
        private bool isColdown = false;
        
        private IInputService _inputService;
        private CharacterController _characterController;
        private Animator _animator; 

        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();

        private void Start()
        {
            _animator = GetComponent<Animator>();
            transform.position = Points[0].position;
        }

        private void Update()
        {
            if ((_inputService.IsKeyDownLeftKey() || _inputService.IsKeyDownLeftArrow()) && !isColdown) 
                Move(0);
            else if ((_inputService.IsKeyDownRightKey() || _inputService.IsKeyDownRightArrow()) && !isColdown) 
                Move(1);
        }
        
        private void Move(int side)
        {
            if (side == 0)
            {
                currentPos = (currentPos - 1 + Points.Length) % Points.Length;
                DoMove();
            }
            else if (side == 1)
            {
                currentPos = (currentPos + 1) % Points.Length;
                DoMove();
            }
            //Debug.Log(currentPos);
        }

        private void DoMove()
        {
            // if (_animationManager.IsAnimationPlaying) return;
            // _animationManager.StartAnimation();
            
            //Sequence jumpTween = DOTween.Sequence();
            
            // _animator.Play("GrabHold");
            // _animator.Play("GrabRelease");
            
            transform.DOJump(Points[currentPos].position, 1, 1, 0.2f);
            
            isColdown = true;
            Invoke("CooldownComplete", cooldownTime);
            
            // jumpTween.OnComplete(() =>_animationManager.StopAnimation());
        }
        
        private void  CooldownComplete() => 
            isColdown = false;

        
        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel() ,transform.position.AsVectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) 
                    Warp(to: savedPosition);
            }
        }
        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}