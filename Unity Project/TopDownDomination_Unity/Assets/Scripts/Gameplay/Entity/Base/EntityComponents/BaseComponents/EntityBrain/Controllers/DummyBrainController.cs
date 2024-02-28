using System;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Controllers
{
    public class DummyBrainController : EntityBrainController
    {
        public override event Action<CombatSkillRequestPackage> OnEntitySkillRequest;
        public override event Action OnEntityInteractRequest;
        public override event Action OnEntityCanceledInteractRequest;
        
        public override Vector3 MoveDirection => Vector3.zero;
        public override Vector3 AimDirection => Vector3.zero;
    }
}