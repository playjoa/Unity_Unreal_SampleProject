using Gameplay.Entity.Base.Abstracts;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimations.Base
{
    public abstract class EntityAnimationController : BaseEntityExtraComponent
    {
        [Header("Target Animator")]
        [SerializeField] protected Animator entityAnimator;

        protected void SetAnimatorBool(int animHash, bool value)
        {
            entityAnimator.SetBool(animHash, value);
        }
        
        protected void SetAnimatorFloat(int animHash, float value)
        {
            entityAnimator.SetFloat(animHash, value);
        }
        
        protected void SetAnimatorTrigger(int animHash)
        {
            entityAnimator.SetTrigger(animHash);
        }
        
        protected void ResetAnimatorTrigger(int animHash)
        {
            entityAnimator.ResetTrigger(animHash);
        }
    }
}