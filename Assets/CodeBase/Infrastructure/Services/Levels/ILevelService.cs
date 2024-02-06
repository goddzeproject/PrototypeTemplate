using UnityEngine;

namespace CodeBase.Infrastructure.Services.Levels
{
    public interface ILevelService : IService
    {
        public GameObject HeroGameObject { get; set; }
        void InitLevelData();

        void InitSpawners(int levelKey);

        void SpawnEnemies();
        
        void InitUIRoot();

        GameObject InitArena();

        void InitHud(GameObject hero);
        GameObject InitHero();

        GameObject InitVirtualCamera();

        void CameraFollow(GameObject vCamera, GameObject _object);

        void InformProgressReaders();


        void CleanUpLevelData();
    }
}