using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Holder;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Logic.Enemy.Behaviour;
using CodeBase.Logic.Enemy.BossBehaviour.Bullets;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Logic.Enemy.BossBehaviour
{
    public class BossBrain : MonoBehaviour
    {
        public PlateSpawner PlateSpawner;
        public BulletSpawner BulletSpawner;
        public EnemyHealth BossHealth;
        public BossMovement BossMovement;

        private Vector3 _direction;
        private IGameFactory _gameFactory;
        private GameObject _boss;
        private IRandomService _randomService;
        private IObjectHolder _objectHolder;
        private Transform _heroTransform;

        private List<GameObject> Waves = new List<GameObject>();
        private List<GameObject> Mines = new List<GameObject>();

        private bool isRightTp;
        private bool isLeftTp;


        /* Логика спавна, когда нужно спавнить и в каких точках арены нужно спавнить пластины? -
        Логика Фазы 1
            Волна - спавн - движение+направление - чек героя - урон - удаление
            Передвижение Босса в другую точку
            Поочередный спавн Пластин.

        Логика Фазы 2
            Бомбочки - спавн - движение+направление - взрыв - чек героя - урон - удаление
            Бомбочки - спавне - движение + направление
            Волна - спавн - движение + направление - активация бомбочек - взрыв - чек героя - урон - удаление
            Поочередный спавн Пластин
        */

        public void Construct(IGameFactory gameFactory, IRandomService randomService, IObjectHolder objectHolder,
            GameObject enemy, Transform heroTransform)
        {
            _gameFactory = gameFactory;
            _objectHolder = objectHolder;
            _boss = enemy;
            _randomService = randomService;
            _heroTransform = heroTransform;
        }

        private void Start()
        {
            SpawnPlate();
            StartCoroutine(BrainUpdate());
            BossHealth.HealthChanged += OnBossHealthChanged;
        }

        private void OnDestroy() =>
            BossHealth.HealthChanged -= OnBossHealthChanged;

        private void OnBossHealthChanged()
        {
            Invoke(nameof(SpawnPlate), 2f);
        }

        private IEnumerator BrainUpdate()
        {
            while (true)
            {
                if (BossHealth.Current > 75f)
                    SpawnWave();
                else if (BossHealth.Current is <= 75f and > 50f)
                {
                    SpawnWave();
                    yield return new WaitForSeconds(0.5f);
                    SpawnWave();
                }
                else if (BossHealth.Current is <= 50f and > 25f)
                {
                    if (!isRightTp) Teleport(new Vector3(8, 0, 0));
                    SpawnMine();
                    SpawnMine();
                }
                else if (BossHealth.Current < 25f)
                {
                    if (!isLeftTp) Teleport(new Vector3(-8, 0, 0));
                    SpawnWave();
                    yield return new WaitForSeconds(0.25f);
                    SpawnWave();
                    SpawnMine();
                }

                yield return new WaitForSeconds(1f);
            }
        }

        private void Teleport(Vector3 point)
        {
            BossMovement.Teleport(point);
            Vector3 _targetDirection = _heroTransform.position - transform.position;
            BossMovement.Rotate(_targetDirection);
        }

        private void SpawnPlate()
        {
            Vector3 randomPosition = _randomService.Position(-6, 6);
            PlateSpawner.Spawn(randomPosition);
        }

        private void SpawnWave()
        {
            var wave = BulletSpawner.SpawnWave();
            Vector3 _targetDirection = _heroTransform.position - transform.position;
            wave.GetComponent<SimpleMovement>().Construct(_targetDirection);
            _objectHolder.RegisterWave(wave);
        }

        private void SpawnMine()
        {
            Vector3 randomPosition = _randomService.Position(-6, 6);
            var mine = BulletSpawner.SpawnMine(randomPosition);
            _objectHolder.RegisterMine(mine);
        }
    }
}