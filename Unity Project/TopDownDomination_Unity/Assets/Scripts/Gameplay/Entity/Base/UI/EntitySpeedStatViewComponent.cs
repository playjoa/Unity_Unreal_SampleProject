using Gameplay.Entity.Base.Data;

namespace Gameplay.Entity.Base.UI
{
    public class EntitySpeedStatViewComponent : EntityStatBarViewComponent
    {
        protected override void OnInitiate(EntityData entityData)
        {
            SetStatFillAmount(entityData.SpeedStatValue / 10f);
        }
    }
}