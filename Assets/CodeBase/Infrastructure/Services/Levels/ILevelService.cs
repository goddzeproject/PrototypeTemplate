﻿using System.Collections.Generic;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public interface ILevelService : IService
    {
        void InitLevelData();

        void InitSpawners(int levelKey);

        void SpawnEnemies();

        GameObject InitArena();

        void InitHud(GameObject hero);
        GameObject InitHero();

        GameObject InitVirtualCamera();

        void CameraFollow(GameObject vCamera, GameObject _object);

        void InformProgressReaders();

        void ClearSpawners();
        void ClearHero();
        void ClearEnemies();
        
        
    }
}