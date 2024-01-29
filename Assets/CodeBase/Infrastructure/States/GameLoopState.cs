using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine stateMachine)
        {
            Exit();
        }

        public void Exit()
        {
            Debug.Log("Hello it's GameLoopState");
        }

        public void Enter()
        {
        }
    }
}