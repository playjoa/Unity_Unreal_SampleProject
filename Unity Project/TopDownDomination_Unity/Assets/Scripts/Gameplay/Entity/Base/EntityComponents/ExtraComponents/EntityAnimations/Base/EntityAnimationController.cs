using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimationSystem.Base
{
    public abstract class EntityAnimationController : BaseEntityExtraComponent
    {
        private Animator _entityAnimator;
        private bool _hasAnimator;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            if (owner.EntityGraphics.EntityAnimator == null) return;
            
            _entityAnimator = owner.EntityGraphics.EntityAnimator;
            _hasAnimator = true;
        }

        protected void SetAnimatorBool(int animHash, bool value)
        {
            if (!_hasAnimator) return;
            
            _entityAnimator.SetBool(animHash, value);
        }
        
        protected void SetAnimatorFloat(int animHash, float value)
        {
            if (!_hasAnimator) return;
            
            _entityAnimator.SetFloat(animHash, value);
        }
        
        protected void SetAnimatorTrigger(int animHash)
        {
            if (!_hasAnimator) return;
            
            _entityAnimator.SetTrigger(animHash);
        }
        
        protected void ResetAnimatorTrigger(int animHash)
        {
            if (!_hasAnimator) return;
            
            _entityAnimator.ResetTrigger(animHash);
        }
    }
}