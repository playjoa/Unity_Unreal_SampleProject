﻿using System.Collections.Generic;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.CombatSkills;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Components;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data
{
    [CreateAssetMenu(menuName = "Entity/Data/Skills/" + nameof(ProjectileCombatSkillData))]
    public class ProjectileCombatSkillData : CombatSkillData
    {
        [Header("Projectile Config.")]
        [SerializeField] private SkillProjectile projectilePrefab;
        [SerializeField] private float projectileVelocity = 150f;
        [SerializeField] private float projectileDistance = 10f;
        [SerializeField] private Vector3 projectileStartOffSet = Vector3.up;

        [Header("Hit Config.")]
        [SerializeField] private List<EntityType> targetsHitEntities = new() { EntityType.Enemy };
        [SerializeField] private List<EntityType> targetsAvoidEntities = new() { EntityType.Player };

        public SkillProjectile ProjectilePrefab => projectilePrefab;
        public float ProjectileVelocity => projectileVelocity;
        public float ProjectileDistance => projectileDistance;
        public Vector3 ProjectileStartOffSet => projectileStartOffSet;
        
        public List<EntityType> TargetsHitEntities => targetsHitEntities;
        public List<EntityType> TargetsAvoidEntities => targetsAvoidEntities;

        public override CombatSkill GenerateCombatSkill(EntityCombatSkillsController skillsController)
        {
            return new ProjectileCombatSkill(this, skillsController);
        }
    }
}