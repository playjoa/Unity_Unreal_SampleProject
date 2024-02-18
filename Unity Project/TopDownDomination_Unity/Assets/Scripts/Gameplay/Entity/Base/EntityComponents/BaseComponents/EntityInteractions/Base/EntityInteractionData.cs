using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base
{
    public abstract class EntityInteractionData : ScriptableObject
    {
        public abstract IEntityInteraction GenerateInteraction(IGameEntity owner, IGameEntity target);
    }
}