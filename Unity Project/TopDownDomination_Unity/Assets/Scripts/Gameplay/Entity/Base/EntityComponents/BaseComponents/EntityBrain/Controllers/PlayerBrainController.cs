using System;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.PlayerInputs.Controller;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Controllers
{
    public class PlayerBrainController : EntityBrainController
    {
        public override event Action<CombatSkillRequestPackage> OnEntitySkillRequest;

        private InputsController InputsController => GameController.ME.InputsController;
        
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