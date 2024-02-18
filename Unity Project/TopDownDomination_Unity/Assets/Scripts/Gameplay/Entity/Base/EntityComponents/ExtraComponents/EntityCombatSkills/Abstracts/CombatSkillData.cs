using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base;
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

        [Header("Skill Config. Data")] 
        [SerializeField] private float coolDown = 5f;

        [Header("Interaction Data")]
        [SerializeField] private EntityInteractionData interactionData;

        public string SkillName => skillName;
        public Sprite SkillIcon => skillIcon;

        public float CoolDown => coolDown;
        
        public EntityInteractionData InteractionData => interactionData;
    }
}