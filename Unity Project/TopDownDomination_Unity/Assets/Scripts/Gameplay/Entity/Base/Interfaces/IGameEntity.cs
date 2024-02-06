﻿using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityGraphics;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement;
using UnityEngine;

namespace Gameplay.Entity.Base.Interfaces
{
    public interface IGameEntity
    {
        bool IsActive { get; }
        EntityType EntityType { get; }
        Transform EntityTransform { get; }
        EntityData EntityData { get; }
        EntityHealth EntityHealth { get; }
        EntityGraphicsController EntityGraphics { get; }
        EntityMovementBase EntityMovement { get; }

        void Initiate(EntityData entityData);
        void Revive();
        
        bool TryGetExtraComponent<TExtraComponent>(out TExtraComponent targetComponent) where TExtraComponent : IEntityExtraComponent;
    }
}