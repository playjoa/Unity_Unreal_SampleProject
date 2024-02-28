using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameVfxSystem.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base
{
    public abstract class EntityInteractionData : ScriptableObject
    {
        [Header("VFXs Config.")] 
        public VfxData vfxToPlayOnOwner;
        public VfxData vfxToPlayOnTarget;

        public abstract IEntityInteraction GenerateInteraction(IGameEntity owner, IGameEntity target);
    }
}