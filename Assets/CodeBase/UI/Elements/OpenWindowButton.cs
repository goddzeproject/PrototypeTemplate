using System;
using CodeBase.Logic.Audio;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public WindowId WindowId;
        private IWindowService _windowsService;

        private AudioManager _audioManager;

        public void Construct(IWindowService windowsService)
        {
            _windowsService = windowsService;
        }

        private void Start() => 
            _audioManager = AudioManager.instance;

        private void Awake() => 
            Button.onClick.AddListener(Open);

        private void Open()
        {
            _audioManager.Play("Button");
            _windowsService.Open(WindowId);
        }
    }
}