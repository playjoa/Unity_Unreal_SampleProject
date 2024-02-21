using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.Entity.Base.Utils;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller
{
    public class EntityCombatSkillsController : BaseEntityExtraComponent
    {
        public Dictionary<CombatSkillType, CombatSkill> CombatSkills { get; private set; }

        public event Action<CombatSkill> OnSkillExecuted;

        private bool _castingSkill = false;
        
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
            CombatSkills = new Dictionary<CombatSkillType, CombatSkill>
            {
                { CombatSkillType.Primary, entityData.GetPrimaryCombatSkill(this) },
                { CombatSkillType.Secondary, entityData.GetSecondaryCombatSkill(this) }
            };
        }

        public void TriggerSkillCooldown(CombatSkill skill, Action onAfterCoolDown)
        {
            StartCoroutine(TriggerSkillCoolDown(skill.BaseData.CoolDown, onAfterCoolDown));
        }

        private IEnumerator TriggerSkillCoolDown(float coolDownTime, Action onAfter)
        {
            yield return new WaitForSeconds(coolDownTime);
            onAfter?.Invoke();
            _castingSkill = false;
        }
        
        private IEnumerator WaitForSkillCast(CombatSkill castingSkill)
        {
            _castingSkill = true;
            yield return new WaitForSeconds(castingSkill.BaseData.CastDuration);
            _castingSkill = false;
        }
        
        private void OnEntityRequestedSkillHandler(CombatSkillRequestPackage requestPackage)
        {
            if (_castingSkill) return;
            if (!CombatSkills.TryGetValue(requestPackage.SkillType, out var combatSkill)) return;
            if (!combatSkill.CanCast) return;
            if (!combatSkill.ExecuteSkill(requestPackage)) return;
            
            StartCoroutine(WaitForSkillCast(combatSkill));
            OnSkillExecuted?.Invoke(combatSkill);
        }
    }
}