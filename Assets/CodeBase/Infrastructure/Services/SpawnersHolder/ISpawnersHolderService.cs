using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ObjectHolder
{
    public interface ISpawnersHolderService : IService
    {
        void RegisterObject(SpawnPoint obj);
        SpawnPoint Take(int number);
    }
}