using System.Collections.Generic;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.UI.Interfaces;
using UnityEngine;
using Utils.SerializableUtils;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.UI
{
    public class PlayerCombatSkillsViewController : MonoBehaviour, IPlayerUI
    {
        [Header("View Config.")] 
        [SerializeField] private SerializableDictio<CombatSkillType, CombatSkillUIView> combatSkillsTargetRects;
        
        private readonly List<CombatSkillUIView> _combatSkillViews = new();
        private EntityCombatSkillsController _playerCombatSkillsController;
        
        public void Initiate(IGameEntity playerEntity)
        {
            if (!playerEntity.TryGetExtraComponent(out _playerCombatSkillsController)) return;
            
            combatSkillsTargetRects.Initiate();
            
            foreach (var (combatSkillType, combatSkillUI) in combatSkillsTargetRects.GeneratedDictionary)
            {
                combatSkillUI.gameObject.SetActive(false);
            }
            
            foreach (var (combatSkillType, combatSkill) in _playerCombatSkillsController.CombatSkills)
            {
                TryInitiateCombatSkillView(combatSkill);
            }
        }

        public void ToggleUI(bool valueToSet)
        {
            gameObject.SetActive(valueToSet);
        }

        private void TryInitiateCombatSkillView(CombatSkill combatSkill)
        {
            if (!combatSkillsTargetRects.TryGetValue(combatSkill.BaseData.SkillType, out var combatSkillUI))
            {
                Debug.LogWarning($"Missing rect for skill: {combatSkill.BaseData.SkillType}");
                return;
            }
            
            combatSkillUI.Initiate(combatSkill);
            _combatSkillViews.Add(combatSkillUI);
        }
    }
}