using UnityEngine;

namespace CodeBase.Logic.Enemy.Behaviour
{
    public class AnimateAlongAcceleration : MonoBehaviour
    {
        private float MinimalVelocity = 0.1f;
        public EnemyAnimator Animator;
        public AccelerationMovement AccelerationMovement ;
            

        private void Update()
        {
            if (ShouldMove())
                Animator.Move(AccelerationMovement.currentSpeed);
            else
            {
                Animator.StopMoving();
            }
        }

        private bool ShouldMove() =>
            AccelerationMovement.currentSpeed > MinimalVelocity;
    }
}