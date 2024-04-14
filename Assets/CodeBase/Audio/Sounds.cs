using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds
{
    public int NumberName;

    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float Volume = 1f;
    
    [Range(0f, 3f)]
    public float Pitch = 1f;

    public bool Loop = false;

    [HideInInspector]
    
    public AudioSource AudioSource;
}