using System;
using UnityEngine;

namespace CodeBase.Logic.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public Sounds[] Sounds;

        public string MainTheme;

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

        private void Start()
        {
            Play(MainTheme);
        }

        public void Play(string SoundName)
        {
            Sounds s = Array.Find(Sounds, Sounds => Sounds.Name == SoundName);
            if (s == null)
            {
                return;
            }
            s.AudioSource.Play();

        }

        public void Stop(string SoundName)
        {
            Sounds s = Array.Find(Sounds, Sounds => Sounds.Name == SoundName);
            if (s == null)
            {
                return;
            }
            s.AudioSource.Stop();
        }
    }
}