using UnityEngine;

namespace CodeBase.Logic.Enemy.Behaviour
{
    public class AccelerationMovement : MonoBehaviour
    {
        public float InitialSpeed = 0;
        public float Acceleration = 0.01f;
        
        private Vector3 _direction;
        public float currentSpeed;
        private float accelerationTimer;


        public void Construct(Vector3 direction)
        {
            _direction = direction;
        }

        private void Start()
        {
            currentSpeed = InitialSpeed;
            accelerationTimer = 0;
            transform.rotation = Quaternion.LookRotation(_direction);
        }

        private void Update()
        {
            accelerationTimer += Time.deltaTime;
            if (accelerationTimer >= 1) 
            {
                currentSpeed += Acceleration;
                accelerationTimer = 0;
            }
            
            transform.Translate(_direction * currentSpeed * Time.deltaTime, Space.World);
        }
    }
}