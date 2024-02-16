using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public interface ILevelService : IService
    {
        public GameObject HeroGameObject { set; get; }
        public GameObject Hud { set; get; }
        void InitLevelData();

        void InitSpawners(int levelKey);

        void SpawnEnemies();

        GameObject InitArena();

        GameObject InitHud(GameObject hero);
        GameObject InitHero();

        GameObject InitVirtualCamera();

        void CameraFollow(GameObject vCamera, GameObject _object);

        void ClearHero();
        void UpdateHud();
        void ClearSpawners();
        void ClearEnemies();
        void InformProgressReaders();
        void ClenUpProgressReaders();

        void OpenRestartWindow();
        void OpenMainMenuWindow();
    }
}