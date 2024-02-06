using UnityEngine;

namespace Gameplay.GameCameraSystem.Data
{
    [CreateAssetMenu(menuName = "Camera/GameCameraConfiguration Data", fileName = "New GameCameraConfigurationData")]
    public class GameCameraConfigurationData : ScriptableObject
    {
        [Header("Camera Position Config.")]
        [SerializeField] private Vector3 playerOffSetPosition = new(4f, 6.5f, 0f);
        [SerializeField] private Vector3 cameraStartRotation = new(60f, -90f, 0f);
        
        [Header("Camera Lens Config.")]
        [SerializeField] private float cameraFov = 65;
        [SerializeField] private float nearClipPlane = 0.001f;
        [SerializeField] private float farClipPlane = 1000f;

        public Vector3 PlayerOffSetPosition => playerOffSetPosition;
        public Vector3 CameraStartRotation => cameraStartRotation;
        
        public float CameraFov => cameraFov;
        public float NearClipPlane => nearClipPlane;
        public float FarClipPlane => farClipPlane;
    }
}