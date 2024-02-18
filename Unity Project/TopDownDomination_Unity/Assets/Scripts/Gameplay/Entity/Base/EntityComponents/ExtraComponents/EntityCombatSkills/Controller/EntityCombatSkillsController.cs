using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller
{
    public class EntityCombatSkillsController : BaseEntityExtraComponent
    {
        public Dictionary<CombatSkillType, CombatSkill> CombatSkills { get; private set; }

        public event Action<CombatSkill> OnSkillExecuted; 
        
        protected override void OnInitiate(IGameEntity owner)
        {
            CreateCombatSkills(owner.EntityData);

            Owner.EntityBrain.OnEntitySkillRequest += OnEntityRequestedSkillHandler;
        }

        protected override void OnClean()
        {
            Owner.EntityBrain.OnEntitySkillRequest -= OnEntityRequestedSkillHandler;
        }

        private void CreateCombatSkills(EntityData entityData)
        {
            CombatSkills = new Dictionary<CombatSkillType, CombatSkill>();
        }

        public void TriggerSkillCooldown(CombatSkill skill, Action onAfterCoolDown)
        {
            if (skill.CanCast) return;

            StartCoroutine(TriggerSkillCoolDown(skill.CombatSkillData.CoolDown, onAfterCoolDown));
        }

        private IEnumerator TriggerSkillCoolDown(float coolDownTime, Action onAfter)
        {
            yield return new WaitForSeconds(coolDownTime);
            onAfter?.Invoke();
        }
        
        private void OnEntityRequestedSkillHandler(CombatSkillRequestPackage requestPackage)
        {
            if (!CombatSkills.TryGetValue(requestPackage.SkillType, out var combatSkill)) return;
            if (!combatSkill.CanCast) return;

            if (combatSkill.ExecuteSkill())
            {
                OnSkillExecuted?.Invoke(combatSkill);
            }
        }
    }
}