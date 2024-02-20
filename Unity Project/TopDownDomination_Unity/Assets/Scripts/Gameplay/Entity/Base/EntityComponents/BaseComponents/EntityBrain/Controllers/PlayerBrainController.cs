using System;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameCameraSystem.Controller;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.PlayerInputs.Controller;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Controllers
{
    public class PlayerBrainController : EntityBrainController
    {
        public override event Action<CombatSkillRequestPackage> OnEntitySkillRequest;
        public override Vector3 MoveDirection => PlayerMovementInCameraDirection(InputsController.PlayerMovementInput);
        public override Vector3 AimDirection => Vector3.zero;

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

        private void OnPlayerTriggeredPrimaryHandler()
        {
            OnEntitySkillRequest?.Invoke(new CombatSkillRequestPackage
            {
                SkillType = CombatSkillType.Primary,
                CastDirection = Owner.EntityTransform.forward,
                CastPosition = Owner.EntityTransform.position
            });
        }
        
        private void OnPlayerTriggeredSecondaryHandler()
        {
            OnEntitySkillRequest?.Invoke(new CombatSkillRequestPackage
            {
                SkillType = CombatSkillType.Secondary,
                CastDirection = Owner.EntityTransform.forward,
                CastPosition = Owner.EntityTransform.position
            });
        }
    }
}