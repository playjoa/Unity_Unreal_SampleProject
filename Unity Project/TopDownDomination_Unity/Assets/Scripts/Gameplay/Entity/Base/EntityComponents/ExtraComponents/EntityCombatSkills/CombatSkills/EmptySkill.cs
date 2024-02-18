using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.CombatSkills
{
    public class EmptySkill : CombatSkill
    {
        public EmptySkill(CombatSkillData data, EntityCombatSkillsController skillsController) : base(data, skillsController)
        {
        }

        protected override void SkillBehaviour()
        {
            
        }
    }
}