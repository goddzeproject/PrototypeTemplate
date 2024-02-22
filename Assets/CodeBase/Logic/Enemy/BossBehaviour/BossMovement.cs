using UnityEngine;

namespace CodeBase.Logic.Enemy.BossBehaviour
{
    public class BossMovement : MonoBehaviour
    {
        public float Speed = 0f;
        private Vector3 _direction;


        public void Construct(Vector3 direction) => 
            _direction = direction;

        private void Start() => 
            Rotate(_direction);

        public Quaternion Rotate(Vector3 direction) => 
            transform.rotation = Quaternion.LookRotation(direction);

        public void Teleport(Vector3 point)
        {
            transform.position = point;
        }
    }
}