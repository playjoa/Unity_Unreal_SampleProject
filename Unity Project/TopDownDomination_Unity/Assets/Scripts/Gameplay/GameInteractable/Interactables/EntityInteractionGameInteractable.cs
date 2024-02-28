using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractableSystem.Abstracts;
using UnityEngine;

namespace Gameplay.GameInteractableSystem.Interactables
{
    public class EntityInteractionGameInteractable : GameInteractable
    {
        [Header("Configuration")]
        [SerializeField] private EntityInteractionData entityInteractionData;

        protected override void OnInteractionStart(IGameEntity targetEntity)
        {
            var entityInteraction = entityInteractionData.GenerateInteraction(targetEntity, targetEntity);
            entityInteraction.ExecuteInteraction();
        }

        protected override bool OnInteractionTickUpdate()
        {
            return false;
        }
    }
}