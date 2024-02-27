using System;
using CodeBase.Logic.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class PauseButton : MonoBehaviour
    {
        public Button Button;
        private AudioManager _audioManager;

        private void Awake() =>
            Button.onClick.AddListener(Resume);

        private void Start() => 
            _audioManager = AudioManager.instance;

        private void Resume()
        {
            _audioManager.Play("Button");
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }
    }
}