using UnityEngine;
using Utils.UniqueId.Components;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data
{
    [CreateAssetMenu(menuName = "Entity/Data/Skills/", fileName = "New CombatSkillData")]
    public class CombatSkillData : ScriptableObjectWithId
    {
        [Header("Meta Data")] 
        [SerializeField] private string skillName = "";
        [SerializeField] private Sprite skillIcon;

        [Header("Skill Config. Data")] 
        [SerializeField] private float coolDown = 5f;

        public string SkillName => skillName;
        public Sprite SkillIcon => skillIcon;

        public float CoolDown => coolDown;
    }
}