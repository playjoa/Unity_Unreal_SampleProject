using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityAnimations.Utils
{
    public static class PlayerAnimations
    {
        // Layers
        public static readonly int LowerBodyLayer = 0;
        public static readonly int UpperBodyLayer = 1;
        
        // Triggers
        public static readonly int TakeHitTrigger = Animator.StringToHash("TakeHitTrigger");
        public static readonly int CastPrimaryTrigger = Animator.StringToHash("CastPrimaryTrigger");
        public static readonly int CastSecondaryTrigger = Animator.StringToHash("CastSecondaryTrigger");

        // Floats
        public static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    }
}