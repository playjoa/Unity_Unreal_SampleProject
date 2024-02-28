using Gameplay.Entity.Base.Interactions;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractableSystem.Abstracts;
using UnityEngine;

namespace Gameplay.GameInteractableSystem.Interactables
{
    public class DamageGameInteractable : GameInteractable
    {
        [Header("Configuration")] 
        [SerializeField] private int damageAmount;
        
        protected override void OnInteractionStart(IGameEntity targetEntity)
        {
            var damageInteraction = new DamageInteraction(targetEntity, targetEntity, damageAmount);
            damageInteraction.ExecuteInteraction();
        }

        protected override bool OnInteractionTickUpdate()
        {
            return false;
        }
    }
}