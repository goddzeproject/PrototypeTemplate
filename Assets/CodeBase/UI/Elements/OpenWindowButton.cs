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

        public void Construct(IWindowService windowsService)
        {
            _windowsService = windowsService;
        }

        private void Awake() => 
            Button.onClick.AddListener(Open);

        private void Open() =>
            _windowsService.Open(WindowId);
    }
}