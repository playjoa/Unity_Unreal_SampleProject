using System;
using System.Collections;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Components;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Utils
{
    public static class CombatSkillsUtils
    {
        private static readonly WaitForEndOfFrame WaitFrame = new();
        
        public static IEnumerator ProjectileLifetime(SkillProjectile projectile, Action onLifeTimeEnded = null)
        {
            var projectileMaxDistance = projectile.ProjectileSkillData.ProjectileDistance;
            var projectileStartingPoint = projectile.StartPosition;
            var traveledMaxDistance = false;

            while (!traveledMaxDistance)
            {
                traveledMaxDistance = Vector3.Distance(projectileStartingPoint, projectile.transform.position) >= projectileMaxDistance;
                yield return WaitFrame;
            }

            onLifeTimeEnded?.Invoke();
        }
    }
}