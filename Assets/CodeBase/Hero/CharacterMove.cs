using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace CodeBase.Hero
{
    public class CharacterMove : MonoBehaviour, ISavedProgress
    {
        public float moveSpeed = 15f;
        public float dashForce = 30f;
        [SerializeField] float acceleration = 2.5f;
        [SerializeField] float decceleration = 3f;
        [SerializeField] float velPower = 0.96f;
        [SerializeField] float frictionAmount = 0.15f;
        [SerializeField] float rotationSpeed = 5f;

        private IInputService _inputService;
        //private CharacterController _characterController;
        
        private Rigidbody _rigidbody;
        private Vector3 startPosition;
        private Quaternion lastDirection;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            //_characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            Movement();
            //Dash();
            Friction();
            
            //DebugSpeed();
        }

        private void Dash()
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                //do charge
                var dashDirection = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y);
                _rigidbody.velocity = dashDirection * dashForce;
            }
            
        }

        private void DebugSpeed()
        {
            //Debug.Log(_inputService.Axis);
            Debug.Log(_inputService.Axis.sqrMagnitude);
        }

        private void Movement()
        {
            float horizontalSpeed = _inputService.Axis.x * moveSpeed;
            float verticalSpeed = _inputService.Axis.y * moveSpeed;
            
            float speedDifX = horizontalSpeed - _rigidbody.velocity.x;
            float speedDifY = verticalSpeed - _rigidbody.velocity.z;
            float accelRateX = (Mathf.Abs(horizontalSpeed) > 0.01f) ? acceleration : decceleration;
            float accelRateY = (Mathf.Abs(verticalSpeed) > 0.01f) ? acceleration : decceleration;
            float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);
            float movementY = Mathf.Pow(Mathf.Abs(speedDifY) * accelRateY, velPower) * Mathf.Sign(speedDifY);
            //_characterController.Move(new Vector3(movementX, 0, movementY) * Time.deltaTime);
            _rigidbody.AddForce(new Vector3(movementX, 0, movementY), ForceMode.Force);
            Rotation();
        }

        private void Rotation()
        {
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                Vector3 direction = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y);
                direction.Normalize();

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                lastDirection = targetRotation;
            }
            else
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, lastDirection, rotationSpeed * Time.deltaTime);
        }

        private void Friction()
        {
            if (Mathf.Abs(_inputService.Axis.x) < 0.01f)
            {
                float amountX = Mathf.Min(Mathf.Abs(_rigidbody.velocity.x), Mathf.Abs(frictionAmount));
                amountX *= Mathf.Sign(_rigidbody.velocity.x);
                _rigidbody.AddForce(Vector3.right * -amountX, ForceMode.Impulse);
            }

            if (Mathf.Abs(_inputService.Axis.y) < 0.01f)
            {
                float amountY = Mathf.Min(Mathf.Abs(_rigidbody.velocity.z), Mathf.Abs(frictionAmount));
                amountY *= Mathf.Sign(_rigidbody.velocity.z);
                _rigidbody.AddForce(Vector3.forward * -amountY, ForceMode.Impulse);
            }
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
            //_characterController.enabled = false;
            //transform.position = to.AsUnityVector().AddY(_characterController.height);
            transform.position = to.AsUnityVector().AddY(2f);
            //_characterController.enabled = true;
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}