﻿using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class PauseButton : MonoBehaviour
    {
        public Button Button;
        private bool isPaused = false;

        private void Awake() =>
            Button.onClick.AddListener(Pause);

        private void Pause()
        {
            isPaused = !isPaused;
            
            if (isPaused)
            {
                // Если пауза включена, устанавливаем Time.timeScale в 0
                Time.timeScale = 0;
            }
            else
            {
                // Если пауза выключена, возвращаем Time.timeScale в 1
                Time.timeScale = 1;
            }
        }
    }
}