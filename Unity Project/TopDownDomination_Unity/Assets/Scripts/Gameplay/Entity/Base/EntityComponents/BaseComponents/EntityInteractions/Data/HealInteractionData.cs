using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Utils;
using Gameplay.Entity.Base.Interactions;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Data
{
    [CreateAssetMenu(menuName = EntityInteractionUtils.ENTITY_INTERACTIONS_PATH + nameof(HealInteractionData))]
    public class HealInteractionData : EntityInteractionData
    {
        [Header("Heal Amount")] 
        public int healAmount;

        public override IEntityInteraction GenerateInteraction(IGameEntity owner, IGameEntity target)
        {
            return new HealInteraction(owner, target, healAmount);
        }
    }
}