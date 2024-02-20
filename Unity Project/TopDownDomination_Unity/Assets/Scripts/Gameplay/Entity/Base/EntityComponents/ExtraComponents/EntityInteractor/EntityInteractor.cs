using System;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.Components;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractable.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityInteractor
{
    public class EntityInteractor : BaseEntityExtraComponent
    {
        [Header("Components")]
        [SerializeField] private InteractableFinderCollider interactableFinder;
        
        public event Action<IGameInteractable> OnInteractableAvailable;
        public event Action<IGameInteractable> OnNoInteractableAvailable;

        private IGameInteractable _currentInteractableInRange;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            if (interactableFinder == null) return;
            
            interactableFinder.OnTriggerEnterInteractable += OnInteractableTriggerEnterHandler;
            interactableFinder.OnTriggerExitInteractable += OnInteractableTriggerExitHandler;
        }

        protected override void OnClean()
        {
            if (interactableFinder == null) return;
            
            interactableFinder.OnTriggerEnterInteractable -= OnInteractableTriggerEnterHandler;
            interactableFinder.OnTriggerExitInteractable -= OnInteractableTriggerExitHandler;
        }
        
        private void OnInteractableTriggerEnterHandler(IGameInteractable gameInteractable)
        {
            OnInteractableAvailable?.Invoke(gameInteractable);
        }

        private void OnInteractableTriggerExitHandler(IGameInteractable gameInteractable)
        {
            OnNoInteractableAvailable?.Invoke(gameInteractable);
        }
    }
}