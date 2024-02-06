using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataEnemiesPath = "StaticData/Enemies";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";
        private Dictionary<EnemyTypeId,EnemyStaticData> _enemies;
        private Dictionary<int,LevelStaticData> _levels;
        private Dictionary<WindowId,WindowConfig> _windowsConfigs;

        public void LoadDataResources()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
                .ToDictionary(x => x.enemyTypeId, x => x);
            
            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _windowsConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public EnemyStaticData ForEnemy(EnemyTypeId typeId) => 
            _enemies.TryGetValue(typeId, out EnemyStaticData staticData) 
                ? staticData 
                : null;

        public LevelStaticData ForLevel(int levelKey) =>
            _levels.TryGetValue(levelKey, out LevelStaticData staticData) 
                ? staticData 
                : null;

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowsConfigs.TryGetValue(windowId, out WindowConfig windowConfig) 
                ? windowConfig 
                : null;
    }
}