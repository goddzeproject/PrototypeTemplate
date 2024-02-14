using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class LevelCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;
        
        private ILevelWatcher _levelWatcher;


        private void Start()
        {
            _levelWatcher = AllServices.Container.Single<ILevelWatcher>();
            UpdateCounter();
        }

        private void Update()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            Counter.text = $"{_levelWatcher.ReturnCurrentLevel()}";
        }
    }
}