using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Logic.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1;
        
        public NavMeshAgent Agent;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        
        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Update() => 
            SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
                Agent.destination = _heroTransform.position;
        }
    }
}