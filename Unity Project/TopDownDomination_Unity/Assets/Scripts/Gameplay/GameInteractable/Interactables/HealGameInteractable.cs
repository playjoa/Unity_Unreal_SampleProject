using Gameplay.Entity.Base.Interactions;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractableSystem.Abstracts;
using UnityEngine;

namespace Gameplay.GameInteractableSystem.Interactables
{
    public class HealGameInteractable : GameInteractable
    {
        [Header("Configuration")] 
        [SerializeField] private int healAmount;
        
        protected override void OnInteractionStart(IGameEntity targetEntity)
        {
            var damageInteraction = new HealInteraction(targetEntity, targetEntity, healAmount);
            damageInteraction.ExecuteInteraction();
        }

        protected override bool OnInteractionTickUpdate()
        {
            return false;
        }
    }
}