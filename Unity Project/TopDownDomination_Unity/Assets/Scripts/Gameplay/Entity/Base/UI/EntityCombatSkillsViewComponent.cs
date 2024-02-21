using Gameplay.Entity.Base.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.UI
{
    public class EntityCombatSkillsViewComponent : EntityViewComponent
    {
        [Header("Images")] 
        [SerializeField] private Image primaryCombatSkillIconImage;
        [SerializeField] private Image secondaryCombatSkillIconImage;

        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI primaryCombatSkillNameTMP;
        [SerializeField] private TextMeshProUGUI secondaryCombatSkillNameTMP;

        protected override void OnInitiate(EntityData entityData)
        {
            SetSkillIcons(entityData);
            SetSkillNames(entityData);
        }

        private void SetSkillIcons(EntityData entityData)
        {
            primaryCombatSkillIconImage.overrideSprite = entityData.PrimarySkillData.SkillIcon;
            secondaryCombatSkillIconImage.overrideSprite = entityData.SecondarySkillData.SkillIcon;
        }
        
        private void SetSkillNames(EntityData entityData)
        {
            primaryCombatSkillNameTMP.text = entityData.PrimarySkillData.SkillName;
            secondaryCombatSkillNameTMP.text = entityData.SecondarySkillData.SkillName;
        }
    }
}