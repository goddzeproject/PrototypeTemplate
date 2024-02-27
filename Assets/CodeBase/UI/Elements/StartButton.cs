using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using CodeBase.Logic.Audio;
using CodeBase.Logic.Hero;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class StartButton : MonoBehaviour
    {
        public Button Button;
        private ILevelService _levelService;

        private AudioManager _audioManager;

        private void Awake()
        {
            _levelService = AllServices.Container.Single<ILevelService>(); // Crutch

            _levelService.HeroGameObject.GetComponent<HeroMove>().enabled = false; // Crutch
            Button.onClick.AddListener(StartGame);
        }

        private void Start() => 
            _audioManager = AudioManager.instance;

        private void StartGame()
        {
            _audioManager.Play("Button");
            _audioManager.Stop("Menu");
            _levelService.HeroGameObject.GetComponent<HeroMove>().enabled = true; // Crutch
            _levelService.SpawnEnemies(); // Crutch
            _audioManager.Play("Game");

            Destroy(gameObject);
        }
    }
}