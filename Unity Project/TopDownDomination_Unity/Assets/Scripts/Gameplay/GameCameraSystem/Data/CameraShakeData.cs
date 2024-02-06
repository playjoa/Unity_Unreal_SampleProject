using UnityEngine;

namespace Gameplay.GameCameraSystem.Data
{
    [CreateAssetMenu(menuName = "Camera/Camera Shake Data", fileName = "New Shake Data")]
    public class CameraShakeData : ScriptableObject
    {
        [Header("Shake Configuration")]
        [SerializeField] private float shakeAmplitude = 1.2f;
        [SerializeField] private float shakeFrequency = 2.0f;
        [SerializeField] private float shakeDuration = 0.25f;
        
        [Header("Override Configuration")]
        [SerializeField] private bool canOverrideOtherShake = false;
        [SerializeField] [Tooltip("Only use for special shakes (Example: cutscenes and such)")] private bool canBeOverriden = true;

        public bool CanOverrideOtherShake => canOverrideOtherShake;
        public bool CanBeOverriden => canBeOverriden;
        public float Amplitude => shakeAmplitude;
        public float Frequency => shakeFrequency;
        public float Duration => shakeDuration;
    }
}