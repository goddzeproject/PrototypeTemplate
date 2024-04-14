using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraColor : MonoBehaviour
    {
        [Range(0, 1)]
        public float saturation = 1f; // Насыщенность цвета
        [Range(0, 1)]
        public float value = 1f; // Яркость цвета
        public float speed = 1f; // Скорость изменения цвета

        private Camera cam;
    
        void Start() => cam = GetComponent<Camera>();

        void Update()
        {
            if (cam != null)
            {
                // Вычисляем новое значение цветового тона на основе времени и скорости
                float hue = Mathf.Repeat(Time.time * speed, 1f);
                // Преобразуем HSV в RGB и применяем полученный цвет фону камеры
                cam.backgroundColor = Color.HSVToRGB(hue, saturation, value);
            }
        }
    }
}