using Gameplay.Entity.Base.Data;

namespace Gameplay.Entity.Base.UI
{
    public class EntityDamageStatViewComponent : EntityStatBarViewComponent
    {
        protected override void OnInitiate(EntityData entityData)
        {
            SetStatFillAmount(entityData.DamageStatValue / EntityData.MAX_STAT_VALUE);
        }
    }
}