﻿using UnityEngine;

namespace CodeBase.Logic.Enemy.Behaviour
{
    public class FollowMovement : MonoBehaviour
    {
        public float Speed = 2f;
        public float TimeFollowing = 5f;
        
        private Transform _target;
        private Vector3 _lastDirection;


        public void Construct(Transform targetPosition) =>
            _target = targetPosition;

        private void Start() =>
            transform.rotation = Quaternion.LookRotation(_target.position);

        private void Update()
        {
            if (TimeFollowing > 0)
                Following();
            else
                SimpleMovement();
        }

        private void SimpleMovement() => 
            transform.Translate(_lastDirection * (Speed * Time.deltaTime), Space.World);

        private void Following()
        {
            Vector3 _targetDirection = _target.position - transform.position;
            _targetDirection.Normalize();
            transform.Translate(_targetDirection * (Speed * Time.deltaTime), Space.World);
            TimeFollowing -= Time.deltaTime;
            
            _lastDirection = _targetDirection;
        }
    }
}