using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.ObjectHolder;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero.Piano
{
    public class PianoKeyLogic : MonoBehaviour
    {
        public string Sound;
        public int Note;
        private AudioManager _audioManager;
        private ISpawnersHolderService _spawnersHolderService;


        private Vector3 standingScale;
        private Vector3 squattingScale;
        private bool isSquatting = false;
        
        

        private void Start()
        {
            _spawnersHolderService = AllServices.Container.Single<ISpawnersHolderService>();
            _audioManager = AudioManager.instance;
            standingScale = transform.localScale;
            squattingScale = new Vector3(standingScale.x,standingScale.y, 0.5f);
        }

        public void Play()
        {
            Squat(squattingScale);
            _audioManager.Play(Sound);
            SpawnNote();

        }

        private void SpawnNote()
        {
            var spawner = _spawnersHolderService.Take(Note);
            spawner.Spawn();
        }



        private void Squat(Vector3 squattingScale)
        {
            if (isSquatting) return;
            isSquatting = true;
            
            Sequence squatSequence = DOTween.Sequence();
            squatSequence.Append(transform.DOScale(squattingScale, 0.2f).SetEase(Ease.InOutQuad));
            squatSequence.Append(transform.DOScale(standingScale, 0.2f).SetEase(Ease.InOutQuad));
            squatSequence.OnComplete(() => isSquatting = false);
            
            
        }
    }
}