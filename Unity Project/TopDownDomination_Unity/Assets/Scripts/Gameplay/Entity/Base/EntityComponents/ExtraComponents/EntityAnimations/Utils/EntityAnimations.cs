using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimationSystem.Utils
{
    public static class EntityAnimations
    {
        // Layers
        public static readonly int LowerBodyLayer = 0;
        public static readonly int UpperBodyLayer = 1;
        
        // Triggers
        public static readonly int TakeHitTrigger = Animator.StringToHash("TakeHitTrigger");
        public static readonly int CastPrimaryTrigger = Animator.StringToHash("CastPrimaryTrigger");
        public static readonly int CastSecondaryTrigger = Animator.StringToHash("CastSecondaryTrigger");
        public static readonly int DeathTrigger = Animator.StringToHash("DeathTrigger");

        // Floats
        public static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    }
}