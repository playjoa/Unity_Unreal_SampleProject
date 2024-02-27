using Gameplay.GameControllerSystem.Controller;
using UnityEngine;

namespace Utils.GameTools
{
    public class LookAtCameraRotator : MonoBehaviour
    {
        private void Awake()
        {
            RotateTowardsCamera();
        }

        private void LateUpdate()
        {
            RotateTowardsCamera();
        }

        private void RotateTowardsCamera()
        {
            if (!GameController.ME.GameCameraController.TryGetMainCameraTransform(out var mainCameraTf)) return;

            var rotation = mainCameraTf.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }
}