using System;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using CodeBase.Infrastructure.States;
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


        private void Awake()
        {
            _levelWatcher = AllServices.Container.Single<ILevelWatcher>();
            _levelService = AllServices.Container.Single<ILevelService>();
            
            Button.onClick.AddListener(Restart);
        }

        private void Start()
        {
            
        }

        private void Restart()
        {
            _levelService.HeroGameObject.transform.position = Vector3.zero;
            _levelWatcher.RestartLevel(1);
            _levelWatcher.ChangeLevel(1);
            Destroy(gameObject);
        }
    }
}