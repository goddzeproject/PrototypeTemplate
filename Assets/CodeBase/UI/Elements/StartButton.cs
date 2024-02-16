using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using CodeBase.Logic.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class StartButton : MonoBehaviour
    {
        public Button Button;
        private ILevelService _levelService;


        private void Awake()
        {
            _levelService = AllServices.Container.Single<ILevelService>(); // Crutch

            _levelService.HeroGameObject.GetComponent<HeroMove>().enabled = false; // Crutch
            Button.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _levelService.HeroGameObject.GetComponent<HeroMove>().enabled = true; // Crutch
            _levelService.SpawnEnemies(); // Crutch

            Destroy(gameObject);
        }
    }
}