using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class FPSCounter : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;
        private float deltaTime;

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";
        }
    }
}