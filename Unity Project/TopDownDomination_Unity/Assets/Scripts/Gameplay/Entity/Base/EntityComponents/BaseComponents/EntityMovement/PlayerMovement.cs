using Gameplay.GameCameraSystem.Controller;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.PlayerInputs.Controller;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement
{
    public class PlayerMovement : EntityMovementBase
    {
        [Header("Character Controller Settings")] 
        [SerializeField] private CharacterController controllerPlayer;
        [SerializeField] private CharacterControllerPhysicsInteraction characterControllerPhysics;

        public override Vector3 EntityMovementDirection => PlayerMovementInCameraDirection(PlayerMovementInput);
        
        private static InputsController InputsController => GameController.ME.InputsController;
        private static GameCameraController CameraController => GameController.ME.GameCameraController;
        private static Vector2 PlayerMovementInput => InputsController.PlayerMovementInput;

        private Vector3 _previousHorizontalPosition;
        private Vector3 _gravityVelocity;
        
        private float _maxSpeed;
        private float _currentHorizontalSprintMultiplier = MovementData.DEFAULT_SPRINT_MULTIPLIER;
        private float _currentVerticalSprintMultiplier = MovementData.DEFAULT_SPRINT_MULTIPLIER;
        
        protected override void OnFixedUpdate()
        {
            GetGroundedState();
        }
        
        private void GetGroundedState()
        {
            var targetGrounded = IsGroundedCheck();

            if (Grounded == targetGrounded) return;
            
            Grounded = targetGrounded;
            InvokeGroundedEvent(Grounded);
        }
        
        private bool IsGroundedCheck()
        {
            return Physics.CheckSphere
            (
                groundCheckerTransform.position,
                movementData.GroundDistance,
                movementData.GroundMask
            );
        }
        
        public override void Teleport(Vector3 teleportPosition, float teleportDuration = 0)
        {
            TogglePlayerController(false);
            Owner.EntityTransform.position = teleportPosition;
            TogglePlayerController(true);
        }
        
        protected override void OnUpdate()
        {
            if (!MovementActive) return;

            MovePlayer(EntityMovementDirection);
        }

        protected override void OnLateUpdate()
        {
            CalculateGravity();
        }

        private void MovePlayer(Vector3 direction)
        {
            controllerPlayer.Move(direction);
        }

        private void TogglePlayerController(bool value)
        {
            controllerPlayer.enabled = value;
        }
        
        private void CalculateGravity()
        {
            if (!GravityActive) return;
            if (groundCheckerTransform == null) return;

            ResetGravityIfGrounded();
            ApplyGravity();
        }
        
        private void ResetGravityIfGrounded()
        {
            if (Grounded && _gravityVelocity.y < 0)
                _gravityVelocity.y = -1.5f;
        }
        
        private void ApplyGravity()
        {
            _gravityVelocity.y += movementData.GravitySpeed * Time.deltaTime;
            MovePlayer(_gravityVelocity * Time.deltaTime);
        }

        private Vector3 PlayerMovementInCameraDirection(Vector2 playerInputValue)
        {
            var inputDirection = CameraController.GetRelativeInputDirectionFromCameraView(playerInputValue);
            inputDirection *= movementData.HorizontalSpeed * Time.deltaTime;

            if (Grounded)
            {
                return inputDirection * _currentHorizontalSprintMultiplier;
            }

            return inputDirection * _currentVerticalSprintMultiplier;
        }
    }
}