using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using UnityEngine;
using Utils.UniqueId.Components;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts
{
    [CreateAssetMenu(menuName = "Entity/Data/Skills/CombatSkillData", fileName = "New CombatSkillData")]
    public abstract class CombatSkillData : ScriptableObjectWithId
    {
        [Header("Meta Data")] 
        [SerializeField] private string skillName = "";
        [SerializeField] private Sprite skillIcon;
        [SerializeField] private CombatSkillType skillType;

        [Header("Skill Config. Data")] 
        [SerializeField] private float castDuration = 1f;
        [SerializeField] private float coolDown = 5f;

        [Header("Interaction Data")]
        [SerializeField] private EntityInteractionData interactionData;

        public string SkillName => skillName;
        public Sprite SkillIcon => skillIcon;
        public CombatSkillType SkillType => skillType;

        public float CastDuration => castDuration;
        public float CoolDown => coolDown;
        
        public EntityInteractionData InteractionData => interactionData;

        public abstract CombatSkill GenerateCombatSkill(EntityCombatSkillsController skillsController);
    }
}