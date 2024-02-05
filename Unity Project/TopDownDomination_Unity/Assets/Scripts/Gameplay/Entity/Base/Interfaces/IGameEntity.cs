using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.Interfaces
{
    public interface IGameEntity
    {
        EntityType EntityType { get; }
        EntityData EntityData { get; }
        EntityHealth EntityHealth { get; }
        Transform EntityTransform { get; }

        void Initiate(EntityData entityData);
        void Revive();
        
        bool TryGetExtraComponent<TExtraComponent>(out TExtraComponent targetComponent) where TExtraComponent : IEntityExtraComponent;
    }
}