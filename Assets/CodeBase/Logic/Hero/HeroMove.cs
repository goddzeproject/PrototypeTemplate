using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public float MovementSpeed;

        private IInputService _inputService;
        private HeroAnimator _heroAnimator;
        private CharacterController _characterController;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            _characterController = GetComponent<CharacterController>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        private void Update()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            Vector3 movementVector = Vector3.zero;
            if (!_heroAnimator.IsAttacking && _inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = new Vector3(_inputService.Axis.x,0 ,_inputService.Axis.y  );
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }


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