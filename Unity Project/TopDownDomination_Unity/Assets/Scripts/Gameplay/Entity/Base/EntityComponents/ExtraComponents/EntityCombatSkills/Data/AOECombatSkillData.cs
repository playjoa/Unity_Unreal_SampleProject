using System.Collections.Generic;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.CombatSkills;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.GameVfxSystem.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data
{
    [CreateAssetMenu(menuName = "Entity/Data/Skills/" + nameof(AOECombatSkillData))]
    public class AOECombatSkillData : CombatSkillData
    {
        [Header("AOE Config.")] 
        [Tooltip("Will execute on caster position, instead of aim position.")] [SerializeField] private bool selfAOE;
        [SerializeField] private float range;

        [Header("VFX Config.")] 
        [SerializeField] private VfxData vfxToPlay;
        
        [Header("Hit Config.")]
        [SerializeField] private List<EntityType> targetsHitEntities = new() { EntityType.Enemy };
        [SerializeField] private List<EntityType> targetsAvoidEntities = new() { EntityType.Player };
        
        public bool SelfAOE => selfAOE;
        public float Range => range;
        
        public VfxData VfxToPlay => vfxToPlay;
        
        public List<EntityType> TargetsHitEntities => targetsHitEntities;
        public List<EntityType> TargetsAvoidEntities => targetsAvoidEntities;

        public override CombatSkill GenerateCombatSkill(EntityCombatSkillsController skillsController)
        {
            if (selfAOE)
            {
                return new SelfAOECombatSkill(this, skillsController);
            }

            return new AOECombatSkill(this, skillsController);
        }
    }
}