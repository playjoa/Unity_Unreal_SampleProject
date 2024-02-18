using System;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base
{
    public abstract class EntityBrainController : BaseEntityComponent
    {
        public abstract event Action<CombatSkillRequestPackage> OnEntitySkillRequest;
    }
}