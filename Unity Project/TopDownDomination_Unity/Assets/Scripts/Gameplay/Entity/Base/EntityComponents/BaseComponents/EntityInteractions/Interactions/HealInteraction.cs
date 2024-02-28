using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Data;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Interactions
{
    public class HealInteraction : EntityInteraction<HealInteractionData>
    {
        public HealInteraction(IGameEntity owner, IGameEntity target, HealInteractionData interactionData) : base(owner, target, interactionData)
        {
        }

        protected override void InteractionBehaviour()
        {
            Target.EntityHealth.UpdateHealth
            (
                new HealthUpdatePackageData
                {
                    Inflicter = Owner,
                    Delta = Mathf.Abs(InteractionData.healAmount)
                }
            );
        }
    }
}