using System.Collections;
using Cinemachine;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameCameraSystem.Components;
using Gameplay.GameCameraSystem.Data;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using UnityEngine;

namespace Gameplay.GameCameraSystem.Controller
{
    public class GameCameraController : MonoBehaviour, IGameplaySystem
    {
        [Header("Target Camera")]
        [SerializeField] private CinemachineVirtualCamera gameVirtualCamera;
        [SerializeField] private Camera mainGameCamera;
        
        [Header("Data")] 
        [SerializeField] private GameCameraConfigurationData cameraConfigurationData;

        public bool CameraShakeActive => _cameraShakeManager.IsShaking;

        private bool _hasCachedMainCamera = false;
        
        private CinemachineTransposer _virtualCameraTransposer;
        private CameraShakeManager _cameraShakeManager;
        private Transform _mainCameraTransform;
        
        public IEnumerator Initiate(GameController gameController)
        {
            Debug.Log("----Initiating CameraController----");

            _virtualCameraTransposer = gameVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            _cameraShakeManager = new CameraShakeManager(gameVirtualCamera);
            
            _mainCameraTransform = mainGameCamera.GetComponent<Transform>();
            _hasCachedMainCamera = true;
            
            SetTargetToEntity(gameController.PlayerEntity);
            SetUpVirtualCamera(gameVirtualCamera, cameraConfigurationData);
            
            yield return new WaitForEndOfFrame();
            
            Debug.Log("----Done Initiating CameraController----");
        }

        public void OnCleanUp()
        {
            
        }
        
        public void SetTargetToEntity(IGameEntity gameEntity)
        {
            var targetOffsetPosition = gameEntity.EntityTransform.position - cameraConfigurationData.PlayerOffSetPosition;
            
            gameVirtualCamera.transform.position = targetOffsetPosition;
            gameVirtualCamera.transform.rotation = Quaternion.Euler(cameraConfigurationData.CameraStartRotation);
            
            gameVirtualCamera.Follow = gameEntity.EntityTransform;
        }
        
        public bool TryGetMainCameraTransform(out Transform mainCameraTf)
        {
            mainCameraTf = _mainCameraTransform;
            return _hasCachedMainCamera;
        }
        
        public Vector3 GetRelativeInputDirectionFromCameraView(Vector2 playerInput)
        {
            var inputDirection = new Vector3(playerInput.x, 0, playerInput.y);

            if (_mainCameraTransform != null)
            {
                inputDirection = Quaternion.Euler(0, _mainCameraTransform.eulerAngles.y, 0) * inputDirection;
            }

            return inputDirection;
        }

        public bool TryGetMainCamera(out Camera mainCamera)
        {
            mainCamera = mainGameCamera;
            return _hasCachedMainCamera;
        }
        
        public Vector3 ConvertWorldPositionToUI(Vector3 worldPosition)
        {
            if (!TryGetMainCamera(out var mainCamera))
            {
                return Vector3.zero;
            }

            return mainCamera.WorldToScreenPoint(worldPosition);
        }

        public void RequestCameraShake(CameraShakeData shakeData)
        {
            _cameraShakeManager.ShakeCamera(shakeData);
        }

        private void Update()
        {
            _cameraShakeManager?.UpdateShake();
        }
        
        private void SetUpVirtualCamera(CinemachineVirtualCamera virtualCamera, GameCameraConfigurationData data)
        {
            virtualCamera.m_Lens.FieldOfView = data.CameraFov;
            virtualCamera.m_Lens.NearClipPlane = data.NearClipPlane;
            virtualCamera.m_Lens.FarClipPlane = data.FarClipPlane;

            if (_virtualCameraTransposer == null) return;

            _virtualCameraTransposer.m_FollowOffset = data.PlayerOffSetPosition;
        }
    }
}