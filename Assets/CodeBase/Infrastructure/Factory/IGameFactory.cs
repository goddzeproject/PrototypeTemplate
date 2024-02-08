using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Enemy;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Logic.Loot;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateArena(GameObject at);
        GameObject CreateHero(GameObject at);
        GameObject CreateVirtualCamera(GameObject at);
        GameObject CreateHud();
        LootPiece CreateLoot();

        SpawnPoint CreateSpawner(Vector3 at, string spawnerId, EnemyTypeId enemyTypeId, Vector3 spawnDirection, int unitsToSpawn, float spawnCooldown, float firstDelay);
        GameObject CreateEnemy(EnemyTypeId typeId, Transform parent, Vector3 direction);

        void CleanUp();
    }
}