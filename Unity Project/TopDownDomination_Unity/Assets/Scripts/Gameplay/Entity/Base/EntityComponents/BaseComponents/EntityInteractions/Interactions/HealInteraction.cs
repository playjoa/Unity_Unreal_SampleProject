using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Interactions
{
    public class HealInteraction : EntityInteraction
    {
        private int _healAmount;
        
        public HealInteraction(IGameEntity owner, IGameEntity target, int healAmount) : base(owner, target)
        {
            _healAmount = Mathf.Abs(healAmount);
        }

        protected override void InteractionBehaviour()
        {
            Target.EntityHealth.UpdateHealth
            (
                new HealthUpdatePackageData
                {
                    Inflicter = Owner,
                    Delta = _healAmount
                }
            );
        }
    }
}