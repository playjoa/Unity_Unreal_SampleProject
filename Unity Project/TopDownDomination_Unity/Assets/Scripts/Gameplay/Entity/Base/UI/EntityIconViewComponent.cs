using Gameplay.Entity.Base.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.UI
{
    public class EntityIconViewComponent : EntityViewComponent
    {
        [Header("View Components")] 
        [SerializeField] private Image playerIconImage;

        protected override void OnInitiate(EntityData entityData)
        {
            SetEntityIconImage(entityData);
        }

        private void SetEntityIconImage(EntityData entityData)
        {
            playerIconImage.overrideSprite = entityData.EntityIcon;
        }
    }
}