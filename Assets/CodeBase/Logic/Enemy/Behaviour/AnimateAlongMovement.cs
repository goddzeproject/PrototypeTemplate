using UnityEngine;

namespace CodeBase.Logic.Enemy.Behaviour
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongMovement : MonoBehaviour
    {
        private float MinimalVelocity = 0.1f;
        public EnemyAnimator Animator;
        public SimpleMovement _SimpleMovement;
            

        private void Update()
        {
            if (ShouldMove())
                Animator.Move(_SimpleMovement.Speed);
            else
            {
                Animator.StopMoving();
            }
        }

        private bool ShouldMove() =>
            _SimpleMovement.Speed > MinimalVelocity;
    }
}