using System;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Audio;
using CodeBase.UI.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class RestartButton : MonoBehaviour
    {
        public Button Button;
        private ILevelWatcher _levelWatcher;
        private ILevelService _levelService;

        private AudioManager _audioManager; 


        private void Awake()
        {
            _levelWatcher = AllServices.Container.Single<ILevelWatcher>();
            _levelService = AllServices.Container.Single<ILevelService>();
            
            Button.onClick.AddListener(Restart);
        }

        private void Start() => 
            _audioManager = AudioManager.instance;

        private void Restart()
        {
            _audioManager.Play("Button");
            
            _levelService.HeroGameObject.transform.position = Vector3.zero;
            _levelWatcher.RestartLevel(1);
            _levelWatcher.ChangeLevel(1);
            
            if(Time.timeScale == 0)
                Time.timeScale = 1;
            _audioManager.Stop("Game");
            _audioManager.Play("Game");
            
            Destroy(gameObject);
        }
    }
}