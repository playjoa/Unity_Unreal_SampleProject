using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimationSystem.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimations.Controllers
{
    public class EntityAnimationsController : EntityAnimationController
    {
        private EntityCombatSkillsController _entitySkillsController;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            base.OnInitiate(owner);
            
            owner.EntityHealth.OnHealthUpdate += OnEntityHealthUpdateHandler;
            owner.EntityHealth.OnDied += OnEntityDiedHandler;

            if (owner.TryGetExtraComponent(out _entitySkillsController))
            {
                _entitySkillsController.OnSkillExecuted += OnPlayerCastedSkillHandler;
            }
        }
        
        protected override void OnClean()
        {
            Owner.EntityHealth.OnHealthUpdate -= OnEntityHealthUpdateHandler;
            Owner.EntityHealth.OnDied -= OnEntityDiedHandler;
            
            if (_entitySkillsController != null)
            {
                _entitySkillsController.OnSkillExecuted -= OnPlayerCastedSkillHandler;
            }
        }

        private void Update()
        {
            if (!Owner.IsActive) return;
            if (Owner.EntityHealth.IsDead) return;
            
            SetAnimatorFloat(EntityAnimationSystem.Utils.EntityAnimations.MoveSpeed, GetPlayerMoveSpeed());
        }
        
        private float GetPlayerMoveSpeed()
        {
            return Owner.EntityMovement.GetHorizontalSpeed();
        }

        private void OnEntityHealthUpdateHandler(HealthChangeData healthChangeData)
        {
            if (healthChangeData.DealtAmount >= 0) return;
            
            SetAnimatorTrigger(EntityAnimationSystem.Utils.EntityAnimations.TakeHitTrigger);
        }
        
        private void OnEntityDiedHandler(HealthChangeData healthChangeData)
        {
            SetAnimatorTrigger(EntityAnimationSystem.Utils.EntityAnimations.DeathTrigger);
        }

        private void OnPlayerCastedSkillHandler(CombatSkill combatSkill)
        {
            switch (combatSkill.BaseData.SkillType)
            {
                case CombatSkillType.Primary:
                    SetAnimatorTrigger(EntityAnimationSystem.Utils.EntityAnimations.CastPrimaryTrigger);
                    break;
                case CombatSkillType.Secondary:
                    SetAnimatorTrigger(EntityAnimationSystem.Utils.EntityAnimations.CastSecondaryTrigger);
                    break;
            }
        }
    }
}