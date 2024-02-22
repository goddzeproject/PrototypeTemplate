using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Logic.Enemy.BossBehaviour
{
    public class PlateSpawner : MonoBehaviour
    {
        private Vector3 _direction;
        private IGameFactory _gameFactory;
        private GameObject _boss;


        public void Construct(IGameFactory gameFactory, GameObject enemy)
        {
            _gameFactory = gameFactory;
            _boss = enemy;
        }

        public GameObject Spawn(Vector3 at)
        {
            var plate = _gameFactory.CreatePlate(at);
            plate.GetComponent<PlateAttack>().Construct(_boss);
            return plate;
        }
    }
}