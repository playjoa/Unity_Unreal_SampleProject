using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Interactions
{
    public class DamageInteraction : EntityInteraction
    {
        private readonly int _damageAmount;
        
        public DamageInteraction(IGameEntity owner, IGameEntity target, int damageAmount) : base(owner, target)
        {
            _damageAmount = Mathf.Abs(damageAmount);
        }

        protected override void InteractionBehaviour()
        {
            Target.EntityHealth.UpdateHealth
            (
                new HealthUpdatePackageData
                {
                    Inflicter = Owner,
                    Delta = -_damageAmount
                }
            );
        }
    }
}