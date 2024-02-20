using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement
{
    public class PlayerMovement : EntityMovementBase
    {
        [Header("Character Controller Settings")] 
        [SerializeField] private CharacterController controllerPlayer;
        [SerializeField] private CharacterControllerPhysicsInteraction characterControllerPhysics;

        private Vector3 _previousHorizontalPosition;
        private Vector3 _gravityVelocity;
        
        private float _maxSpeed;
        private float _currentHorizontalSprintMultiplier = MovementData.DEFAULT_SPRINT_MULTIPLIER;
        private float _currentVerticalSprintMultiplier = MovementData.DEFAULT_SPRINT_MULTIPLIER;

        protected override void OnInitiate(IGameEntity owner)
        {
            base.OnInitiate(owner);
            Grounded = IsGroundedCheck();
        }

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

        public override float GetHorizontalSpeed()
        {
            var playerPosition = transform.position;
            var currentHorizontalPosition = new Vector3(playerPosition.x, 0f, playerPosition.z);
            var distance = Vector3.Distance(currentHorizontalPosition, _previousHorizontalPosition);
            var speed = distance / Time.deltaTime;
            _previousHorizontalPosition = currentHorizontalPosition;

            return Mathf.Clamp01(speed / _maxSpeed);
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
            direction *= movementData.HorizontalSpeed;

            if (Grounded)
            {
                direction *= _currentHorizontalSprintMultiplier;
            }

            else
            {
                direction *= _currentVerticalSprintMultiplier;
            }

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
    }
}