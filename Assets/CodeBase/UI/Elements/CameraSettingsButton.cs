using System;
using Cinemachine;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class CameraSettingsButton : MonoBehaviour
    {
        public Button Button;
        public TextMeshProUGUI CameraFov; 
        
        private ILevelService _levelService;
        private CinemachineVirtualCamera _virtualCamera;
        private bool OnMobile;

        private void Awake()
        {
            _levelService = AllServices.Container.Single<ILevelService>();
            _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            
            CameraFov.text = $"FOV: {(_virtualCamera.m_Lens.FieldOfView)}";
            Button.onClick.AddListener(AdaptateFoV);
        }

        private void AdaptateFoV()
        {
            
            if (!OnMobile)
            {
                _virtualCamera.m_Lens.FieldOfView = 65f;
                OnMobile = true;
            }
            else
            {
                _virtualCamera.m_Lens.FieldOfView = 40f;
                OnMobile = false;
            }
            CameraFov.text = $"FOV: {(_virtualCamera.m_Lens.FieldOfView)}";
        }
    }
}