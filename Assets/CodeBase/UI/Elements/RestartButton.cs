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
        

        private void Awake()
        {
            _levelWatcher = AllServices.Container.Single<ILevelWatcher>();
            
            Button.onClick.AddListener(Restart);
        }

        private void Start()
        {
            
        }

        private void Restart()
        {
            _levelWatcher.RestartLevel(1);
            _levelWatcher.ChangeLevel(1);
            Destroy(gameObject);
        }
    }
}