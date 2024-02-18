using System;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts
{
    public abstract class CombatSkill
    {
        public CombatSkillData CombatSkillData { get; }
        public IGameEntity Owner { get; }
        public EntityCombatSkillsController SkillsController { get; }

        public bool CanCast { get; private set; } = true;
        
        public event Action<CombatSkill> OnSkillExecuted;
        public event Action<CombatSkill> OnSkillCooldownReset;

        protected CombatSkill(CombatSkillData data, EntityCombatSkillsController skillsController)
        {
            CombatSkillData = data;
            SkillsController = skillsController;
            Owner = skillsController.Owner;
        }

        public bool ExecuteSkill()
        {
            if (!CanCast) return false;

            CanCast = false;
            SkillBehaviour();
            SkillsController.TriggerSkillCooldown(this, ResetCooldown);
            OnSkillExecuted?.Invoke(this);

            return true;
        }

        private void ResetCooldown()
        {
            CanCast = true;
            OnSkillCooldownReset?.Invoke(this);
        }

        protected abstract void SkillBehaviour();
    }
}