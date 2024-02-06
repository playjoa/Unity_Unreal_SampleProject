using Cinemachine;
using Gameplay.GameCameraSystem.Data;
using UnityEngine;

namespace Gameplay.GameCameraSystem.Components
{
    public class CameraShakeManager
    {
        public bool IsShaking => _currentCameraShakeData != null;
        
        private CameraShakeData _currentCameraShakeData;
        private readonly CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
        private float _shakeTimeLeft;

        public CameraShakeManager(CinemachineVirtualCamera targetVirtualCamera)
        {
            if (targetVirtualCamera != null)
                _virtualCameraNoise = targetVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(CameraShakeData shakeData)
        {
            if (shakeData == null) return;
            if (IsShaking && !_currentCameraShakeData.CanBeOverriden) return;
            if (IsShaking && !shakeData.CanOverrideOtherShake) return;
            
            _currentCameraShakeData = shakeData;
            _shakeTimeLeft = shakeData.Duration;
        }

        public void UpdateShake()
        {
            if (_virtualCameraNoise == null) return;
            ProcessShake();
        }

        private void ProcessShake()
        {
            if (!IsShaking) return;

            _shakeTimeLeft -= Time.deltaTime;

            var amplitudeToSet = Mathf.Lerp(_currentCameraShakeData.Amplitude, 0f, ShakeProgress());
            _virtualCameraNoise.m_AmplitudeGain = amplitudeToSet;
            _virtualCameraNoise.m_FrequencyGain = _currentCameraShakeData.Frequency;

            if (!(_shakeTimeLeft <= 0)) return;
            
            _virtualCameraNoise.m_AmplitudeGain = 0f;
            _virtualCameraNoise.m_FrequencyGain = 0f;
            _currentCameraShakeData = null;

            float ShakeProgress() => _shakeTimeLeft / _currentCameraShakeData.Duration;
        }
    }
}