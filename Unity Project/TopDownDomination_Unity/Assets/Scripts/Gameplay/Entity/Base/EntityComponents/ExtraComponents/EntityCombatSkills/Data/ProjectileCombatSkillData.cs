using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data
{
    [CreateAssetMenu(menuName = "Entity/Data/Skills/" + nameof(ProjectileCombatSkillData), fileName = "New CombatSkillData")]
    public class ProjectileCombatSkillData : CombatSkillData
    {
        [SerializeField] private Vector3 bulletStartOffSet = Vector3.up;
        
        public Vector3 BulletStartOffSet => bulletStartOffSet;
    }
}