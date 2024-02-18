using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Utils;
using Gameplay.Entity.Base.Interactions;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Data
{
    [CreateAssetMenu(menuName = EntityInteractionUtils.ENTITY_INTERACTIONS_PATH + nameof(DamageInteractionData))]
    public class DamageInteractionData : EntityInteractionData
    {
        [Header("Damage Amount")] 
        public int damageAmount;
        
        public override IEntityInteraction GenerateInteraction(IGameEntity owner, IGameEntity target)
        {
            return new DamageInteraction(owner, target, damageAmount);
        }
    }
}