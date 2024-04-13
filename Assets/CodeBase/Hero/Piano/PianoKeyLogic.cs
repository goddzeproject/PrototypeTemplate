using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero.Piano
{
    public class PianoKeyLogic : MonoBehaviour
    {
        private AudioManager _audioManager;

        public string Sound;

        private void Start() => 
            _audioManager = AudioManager.instance;

        public void Play()
        {
            _audioManager.Play(Sound);
            
            //Animation key
        }
    }
}