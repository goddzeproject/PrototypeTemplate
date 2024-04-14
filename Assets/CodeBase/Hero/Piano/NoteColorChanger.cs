using UnityEngine;

namespace CodeBase.Hero.Piano
{
    public class NoteColorChanger : MonoBehaviour
    {
        public float duration = 5f; // Продолжительность полного цикла изменения цвета
        public Material material;

        void Start()
        {
            if (material == null) Debug.LogError("Renderer component missing from the object!");
        }

        void Update()
        {
            if (material != null)
            {
                // Плавно изменяем оттенок от 0 до 1 за указанную продолжительность
                float hue = Mathf.Repeat(Time.time / duration, 1f);
                Color color = Color.HSVToRGB(hue, 1f, 1f);
                material.color = color;
            }
        }
    }
}