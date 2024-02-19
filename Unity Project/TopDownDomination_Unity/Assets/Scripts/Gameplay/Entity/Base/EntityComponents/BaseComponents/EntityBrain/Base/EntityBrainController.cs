using System;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base
{
    public abstract class EntityBrainController : BaseEntityComponent
    {
        public abstract event Action<CombatSkillRequestPackage> OnEntitySkillRequest;
        public abstract Vector3 MoveDirection { get; }
        public abstract Vector3 AimDirection { get; }
    }
}