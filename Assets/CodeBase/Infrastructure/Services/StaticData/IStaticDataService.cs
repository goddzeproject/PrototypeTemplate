using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadDataResources();
        EnemyStaticData ForEnemy(EnemyTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId windowId);
    }
}