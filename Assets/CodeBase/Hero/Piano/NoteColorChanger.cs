using UnityEngine;

namespace CodeBase.Hero.Piano
{
    public class NoteColorChanger : MonoBehaviour
    {
        public float duration = 5f;
        public Material material;

        void Start()
        {
            if (material == null) Debug.LogError("Renderer component missing from the object!");
        }

        void Update()
        {
            if (material != null)
            {
                float hue = Mathf.Repeat(Time.time / duration, 1f);
                Color color = Color.HSVToRGB(hue, 1f, 1f);
                material.color = color;
            }
        }
    }
}