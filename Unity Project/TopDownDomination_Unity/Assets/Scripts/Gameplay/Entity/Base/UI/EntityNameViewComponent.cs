using Gameplay.Entity.Base.Data;
using TMPro;
using UnityEngine;

namespace Gameplay.Entity.Base.UI
{
    public class EntityNameViewComponent : EntityViewComponent
    {
        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI playerNameTMP;

        protected override void OnInitiate(EntityData entityData)
        {
            SetEntityNameText(entityData);
        }

        private void SetEntityNameText(EntityData entityData)
        {
            playerNameTMP.text = entityData.EntityName;
        }
    }
}