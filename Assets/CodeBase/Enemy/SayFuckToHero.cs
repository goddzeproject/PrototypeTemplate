using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class SayFuckToHero : Follow
    {
        private IGameFactory _gameFactory;
        private string heroName;
        
        public void Construct(Transform heroTransform) => 
            heroName = heroTransform.name;
        

        private void OnTriggerExit(Collider other)
        {
            if (other.name == heroName)
                Debug.Log("Fuck you !" + heroName);
        }
    }
}