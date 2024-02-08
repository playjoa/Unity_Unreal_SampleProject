using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimations.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimations.Utils;
using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimations.Controllers
{
    public class PlayerEntityAnimatorController : EntityAnimationController
    {
        // private EntitySkillsController _entitySkillsController;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            owner.EntityHealth.OnHealthUpdate += OnPlayerHealthUpdateHandler;

            /*if (owner.TryGetExtraComponent(out _entitySkillsController))
            {
                _entitySkillsController.OnPrimarySkillCast += OnPrimarySkillCastedHandler;
                _entitySkillsController.OnSecondarySkillCast += OnSecondarySkillCastedHandler;
            }*/
        }
        
        private void Update()
        {
            if (!Owner.IsActive) return;
            if (Owner.EntityHealth.IsDead) return;
            
            SetAnimatorFloat(PlayerAnimations.MoveSpeed, GetPlayerMoveSpeed());
        }
        
        private float GetPlayerMoveSpeed()
        {
            return Owner.EntityMovement.GetHorizontalSpeed();
        }

        private void OnPlayerHealthUpdateHandler(HealthChangeData healthChangeData)
        {
            if (healthChangeData.DealtAmount >= 0) return;
            
            SetAnimatorTrigger(PlayerAnimations.TakeHitTrigger);
        }

        protected override void OnClean()
        {
            /*if (_entitySkillsController != null)
            {
                _entitySkillsController.OnPrimarySkillCast -= OnPrimarySkillCastedHandler;
                _entitySkillsController.OnSecondarySkillCast -= OnSecondarySkillCastedHandler;
            }*/
        }
        
        // TODO - Link Skills Here
        /*private void OnPrimarySkillCastedHandler(Skill skill)
        {
            SetAnimatorTrigger(PlayerAnimations.CastPrimaryTrigger);
        }
        
        private void OnSecondarySkillCastedHandler(Skill skill)
        {
            SetAnimatorTrigger(PlayerAnimations.CastSecondaryTrigger);
        }*/
    }
}