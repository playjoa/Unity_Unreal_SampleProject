using System;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Controllers
{
    public class DummyBrainController : EntityBrainController
    {
        public override event Action<CombatSkillRequestPackage> OnEntitySkillRequest;
    }
}