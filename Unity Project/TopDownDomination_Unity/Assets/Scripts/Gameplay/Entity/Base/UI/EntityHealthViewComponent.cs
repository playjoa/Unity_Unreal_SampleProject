using Gameplay.Entity.Base.Data;
using TMPro;
using UnityEngine;
using Utils.Extensions;

namespace Gameplay.Entity.Base.UI
{
    public class EntityHealthViewComponent : EntityViewComponent
    {
        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI healthValueTMP;

        protected override void OnInitiate(EntityData entityData)
        {
            SetEntityHealthText(entityData);
        }

        private void SetEntityHealthText(EntityData entityData)
        {
            healthValueTMP.text = entityData.HealthData.MaxHealthAmount.ToNiceCurrency();
        }
    }
}