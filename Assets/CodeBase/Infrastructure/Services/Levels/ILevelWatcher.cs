using System.Collections;
using CodeBase.Logic.EnemySpawners;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public interface ILevelWatcher : IService
    {
        int ReturnCurrentLevel();
        void RestartLevel(int levelKey);
        void StartWatching();
        void ChangeLevel(int levelKey);
    }
}