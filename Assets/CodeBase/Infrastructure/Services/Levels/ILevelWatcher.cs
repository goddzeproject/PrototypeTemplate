using CodeBase.Logic.EnemySpawners;

namespace CodeBase.Infrastructure.Services.Levels
{
    public interface ILevelWatcher : IService
    {
        void Register(SpawnPoint spawnPoint);
        void UnRegister(SpawnPoint spawnPoint);
    }
}