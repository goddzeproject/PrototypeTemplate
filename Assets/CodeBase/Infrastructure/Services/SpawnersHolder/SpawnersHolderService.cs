using System.Collections.Generic;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ObjectHolder
{
    public class SpawnersHolderService : ISpawnersHolderService
    {
        public List<SpawnPoint> Objects = new List<SpawnPoint>();
        
        public void RegisterObject(SpawnPoint obj) => 
            Objects.Add(obj);

        public SpawnPoint Take(int number) => 
            Objects[number];
    }
}