using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class PauseButton : MonoBehaviour
    {
        public Button Button;

        private void Awake() =>
            Button.onClick.AddListener(Resume);

        private void Resume()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }
    }
}