using System;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameCameraSystem.Controller;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.PlayerInputs.Controller;
using UnityEngine;
using Utils.Extensions;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Controllers
{
    public class PlayerBrainController : EntityBrainController
    {
        public override event Action<CombatSkillRequestPackage> OnEntitySkillRequest;
        public override Vector3 MoveDirection => PlayerMovementInCameraDirection(InputsController.PlayerMovementInput);
        public override Vector3 AimDirection => PlayerAimDirection();
        
        private InputsController InputsController => GameController.ME.InputsController;
        private static GameCameraController CameraController => GameController.ME.GameCameraController;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            InputsController.OnPrimaryFire += OnPlayerTriggeredPrimaryHandler;
            InputsController.OnStartSecondaryFire += OnPlayerTriggeredSecondaryHandler;
        }

        protected override void OnClean()
        {
            InputsController.OnPrimaryFire -= OnPlayerTriggeredPrimaryHandler;
            InputsController.OnStartSecondaryFire -= OnPlayerTriggeredSecondaryHandler;
        }

        private Vector3 PlayerMovementInCameraDirection(Vector2 playerInputValue)
        {
            var inputDirection = CameraController.GetRelativeInputDirectionFromCameraView(playerInputValue);
            return inputDirection * Time.deltaTime;
        }

        private Vector3 PlayerAimDirection()
        {
            var worldPos = CameraController.GetWorldPositionFromUI(InputsController.PlayerAimInput);
            var targetDirection = (worldPos - Owner.EntityTransform.position).normalized;
            return  new Vector2(targetDirection.x, targetDirection.z);
        }

        private void OnPlayerTriggeredPrimaryHandler()
        {
            OnEntitySkillRequest?.Invoke(new CombatSkillRequestPackage
            {
                SkillType = CombatSkillType.Primary,
                CastRotation = Owner.EntityGraphics.EntityGraphicsHolder.rotation,
                CastDirection = Owner.EntityGraphics.EntityGraphicsHolder.forward,
                CasterPosition = Owner.EntityTransform.position,
                WorldPosition = CameraController.GetWorldPositionFromUI(InputsController.PlayerAimInput).SetY(0)
            });
        }
        
        private void OnPlayerTriggeredSecondaryHandler()
        {
            OnEntitySkillRequest?.Invoke(new CombatSkillRequestPackage
            {
                SkillType = CombatSkillType.Secondary,
                CastRotation = Owner.EntityGraphics.EntityGraphicsHolder.rotation,
                CastDirection = Owner.EntityGraphics.EntityGraphicsHolder.forward,
                CasterPosition = Owner.EntityTransform.position,
                WorldPosition = CameraController.GetWorldPositionFromUI(InputsController.PlayerAimInput).SetY(0)
            });
        }
    }
}