using UnityEngine;

namespace CodeBase.Logic.Enemy.Behaviour
{
    public class ChargeMovement : MonoBehaviour
    {
        public float Speed = 2f;
        public float amplitude = 1f;

        private float startTime;
        private Vector3 _direction;

        private void Start()
        {
            startTime = Time.time;
            transform.rotation = Quaternion.LookRotation(_direction);
        }

        public void Construct(Vector3 direction) => 
            _direction = direction;

        private void Update()
        {
            float offset = Mathf.Sin((Time.time - startTime) * Speed) * amplitude;

            transform.Translate(_direction * Speed * Time.deltaTime, Space.World);
            transform.Translate((_direction * offset) * Speed * Time.deltaTime, Space.World);
        }
    }
}