
using UnityEngine;

namespace CodeBase.Logic.Enemy.Behaviour
{
    public class SimpleMovement : MonoBehaviour
    {
        public float Speed = 1f;
        private Vector3 _direction;


        public void Construct(Vector3 direction)
        {
            _direction = direction;
        }

        private void Start()
        {
            //transform.LookAt(transform.position + _direction);
            transform.rotation = Quaternion.LookRotation(_direction);
        }

        private void Update()
        {
            transform.Translate(_direction * Speed * Time.deltaTime, Space.World);
        }
    }
}