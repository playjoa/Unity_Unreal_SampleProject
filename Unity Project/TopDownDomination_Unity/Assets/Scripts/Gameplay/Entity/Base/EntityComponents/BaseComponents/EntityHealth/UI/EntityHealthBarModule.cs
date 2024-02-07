using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.Components.UI
{
    public class EntityHealthBarModule : EntityHealthModuleUI
    {
        [Header("Image Feedback")]
        [SerializeField] private Image healthBarImage;
        
        protected override void OnInitiated(EntityHealth healthOwner)
        {
            FillHealthValue(healthOwner.HealthPercentage);
        }

        private void FillHealthValue(float healthPercent)
        {
            healthBarImage.fillAmount = healthPercent;
        }

        public override void OnHealthUpdate(HealthChangeData healthEventData)
        {
            FillHealthValue(healthEventData.Victim.EntityHealth.HealthPercentage);
        }
    }
}