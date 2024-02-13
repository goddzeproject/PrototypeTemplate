using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Levels;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class LevelCounter : MonoBehaviour
    {
        public TextMeshProUGUI Counter;

        private ILevelWatcher _levelWatcher;

        public void Construct( ILevelWatcher levelWatcher)
        {
            _levelWatcher = levelWatcher;
        }

        private void Start()
        {
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