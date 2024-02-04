using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private GameStateMachine _stateMachine;
        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            Exit();
        }

        public void Exit()
        {
            Debug.Log("Hello it's GameLoopState");
        }
        
        public void Enter()
        {
            //_stateMachine.Enter<BootstrapState>();
        }
        
        
    }
}