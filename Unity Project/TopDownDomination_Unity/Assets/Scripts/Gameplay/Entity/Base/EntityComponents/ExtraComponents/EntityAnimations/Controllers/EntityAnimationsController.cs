using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimationSystem.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimationSystem.Utils;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimationSystem.Controllers
{
    public class EntityAnimationsController : EntityAnimationController
    {
        private EntityCombatSkillsController _entitySkillsController;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            base.OnInitiate(owner);
            
            owner.EntityHealth.OnHealthUpdate += OnPlayerHealthUpdateHandler;

            if (owner.TryGetExtraComponent(out _entitySkillsController))
            {
                _entitySkillsController.OnSkillExecuted += OnPlayerCastedSkillHandler;
            }
        }
        
        private void Update()
        {
            if (!Owner.IsActive) return;
            if (Owner.EntityHealth.IsDead) return;
            
            SetAnimatorFloat(EntityAnimations.MoveSpeed, GetPlayerMoveSpeed());
        }
        
        private float GetPlayerMoveSpeed()
        {
            return Owner.EntityMovement.GetHorizontalSpeed();
        }

        private void OnPlayerHealthUpdateHandler(HealthChangeData healthChangeData)
        {
            if (healthChangeData.DealtAmount >= 0) return;
            
            SetAnimatorTrigger(EntityAnimations.TakeHitTrigger);
        }

        protected override void OnClean()
        {
            if (_entitySkillsController != null)
            {
                _entitySkillsController.OnSkillExecuted -= OnPlayerCastedSkillHandler;
            }
        }

        private void OnPlayerCastedSkillHandler(CombatSkill combatSkill)
        {
            switch (combatSkill.BaseData.SkillType)
            {
                case CombatSkillType.Primary:
                    SetAnimatorTrigger(EntityAnimations.CastPrimaryTrigger);
                    break;
                case CombatSkillType.Secondary:
                    SetAnimatorTrigger(EntityAnimations.CastSecondaryTrigger);
                    break;
            }
        }
    }
}