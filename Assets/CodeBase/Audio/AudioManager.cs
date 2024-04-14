using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sounds[] Sounds;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);

        foreach(Sounds sound in Sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.clip;
            sound.AudioSource.volume = sound.Volume;
            sound.AudioSource.pitch = sound.Pitch;
            sound.AudioSource.loop = sound.Loop;
        }
    }

    public void Play(int SoundName)
    {
        Sounds s = Array.Find(Sounds, Sounds => Sounds.NumberName == SoundName);
        if (s == null)
        {
            return;
        }
        s.AudioSource.Play();

    }


}