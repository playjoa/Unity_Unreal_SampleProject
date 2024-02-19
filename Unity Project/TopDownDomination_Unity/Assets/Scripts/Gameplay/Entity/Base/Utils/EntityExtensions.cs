using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.CombatSkills;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;

namespace Gameplay.Entity.Base.Utils
{
    public static class EntityExtensions
    {
        public static CombatSkill GetPrimaryCombatSkill(this EntityData data, EntityCombatSkillsController skillsController)
        {
            return data.PrimarySkillData == null
                ? new EmptySkill(skillsController)
                : data.PrimarySkillData.GenerateCombatSkill(skillsController);
        }

        public static CombatSkill GetSecondaryCombatSkill(this EntityData data, EntityCombatSkillsController skillsController)
        {
            return data.SecondarySkillData == null
                ? new EmptySkill(skillsController)
                : data.SecondarySkillData.GenerateCombatSkill(skillsController);
        }
    }
}