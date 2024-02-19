using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Components;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.CombatSkills
{
    public class ProjectileCombatSkill : CombatSkill<ProjectileCombatSkillData>
    {
        private readonly SkillProjectile _projectilePrefab;
        private readonly bool _hasProjectilePrefab;
        
        public ProjectileCombatSkill(ProjectileCombatSkillData data, EntityCombatSkillsController skillsController) : base(data, skillsController)
        {
            if (data.ProjectilePrefab == null) return;
            
            _projectilePrefab = data.ProjectilePrefab;
            _hasProjectilePrefab = true;
        }

        protected override void SkillBehaviour(CombatSkillRequestPackage requestPackage)
        {
            if (!_hasProjectilePrefab) return;

            var position = requestPackage.CastPosition + CombatSkillData.ProjectileStartOffSet;
            var rotation = requestPackage.CastRotation;
            var spawnedProjectile = Object.Instantiate(_projectilePrefab, position, rotation);
            
            spawnedProjectile.Initiate(Owner, CombatSkillData);
        }
    }
}