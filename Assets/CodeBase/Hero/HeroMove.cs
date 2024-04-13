using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public Transform[] Points;
        public int currentPos = 0;
        
        private IInputService _inputService;
        private HeroAnimator _heroAnimator;
        private CharacterController _characterController;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        private void Start()
        {
            transform.position = Points[0].position;
        }

        private void Update()
        {
            if (_inputService.IsKeyDownLeft() && !_heroAnimator.IsAttacking) 
                Move(0);
            else if (_inputService.IsKeyDownRight() && !_heroAnimator.IsAttacking) 
                Move(1);
        }
        
        private void Move(int side)
        {
            if (side == 0)
            {
                // currentPos++;
                // if (currentPos < 0)
                //     currentPos = Points.Length - 1;
                // transform.position = Points[currentPos].position;
                currentPos = (currentPos - 1 + Points.Length) % Points.Length;
                transform.position = Points[currentPos].position;
            }
            else if (side == 1)
            {
                // currentPos--;
                // if (currentPos >= Points.Length)
                //     currentPos = Points.Length;
                // transform.position = Points[currentPos].position;
                currentPos = (currentPos + 1) % Points.Length;
                transform.position = Points[currentPos].position;
            }
            Debug.Log(currentPos);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel() ,transform.position.AsVectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) 
                    Warp(to: savedPosition);
            }
            
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}