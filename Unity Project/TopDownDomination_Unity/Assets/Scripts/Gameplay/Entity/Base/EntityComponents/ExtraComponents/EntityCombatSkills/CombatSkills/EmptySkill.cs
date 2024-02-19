using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.CombatSkills
{
    public class EmptySkill : CombatSkill
    {
        public EmptySkill(EntityCombatSkillsController skillsController) : base(default, skillsController)
        {
        }

        protected override void SkillBehaviour(CombatSkillRequestPackage requestPackage)
        {
            
        }
    }
}