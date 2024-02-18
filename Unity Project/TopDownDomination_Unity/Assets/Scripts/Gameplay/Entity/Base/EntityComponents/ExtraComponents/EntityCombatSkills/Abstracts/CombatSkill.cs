using System;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts
{
    public abstract class CombatSkill
    {
        public CombatSkillData CombatSkillData { get; }
        public IGameEntity Owner { get; }

        public event Action<CombatSkill> OnSkillExecuted;

        protected CombatSkill(CombatSkillData data, IGameEntity owner)
        {
            CombatSkillData = data;
            Owner = owner;
        }

        public void ExecuteSkill()
        {
            SkillBehaviour();
            OnSkillExecuted?.Invoke(this);
        }

        protected abstract void SkillBehaviour();
    }
}