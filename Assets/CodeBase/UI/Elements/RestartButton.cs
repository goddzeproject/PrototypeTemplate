using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class RestartButton : MonoBehaviour
    {
        public Button Button;

        private void Awake() => 
            Button.onClick.AddListener(Restart);

        private void Restart()
        {
            
        }
    }
}