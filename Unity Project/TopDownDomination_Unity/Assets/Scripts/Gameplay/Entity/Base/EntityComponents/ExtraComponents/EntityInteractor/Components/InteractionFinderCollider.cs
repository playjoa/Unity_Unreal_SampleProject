using System;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.Entity.Base.Utils;
using Gameplay.GameInteractableSystem.Controller;
using Gameplay.GameInteractableSystem.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityInteractor.Components
{
    public class InteractionFinderCollider : MonoBehaviour
    {
        public event Action<IGameInteractable> OnInteractableInRange;
        public event Action<IGameInteractable> OnInteractableOutOfRange;

        public IGameInteractable CurrentInteractable { get; private set; }
        private IGameEntity _owner;
        private EntityGameInteractorController _gameInteractorController;
        
        public void Initiate(EntityGameInteractorController interactorController)
        {
            _gameInteractorController = interactorController;
            _owner = interactorController.Owner;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!IsValidInteractable(other.gameObject, out var gameInteractable)) return;

            CurrentInteractable = gameInteractable;
            CurrentInteractable.OnEndInteraction += OnCurrentInteractableEndInteractionHandler;
            
            OnInteractableInRange?.Invoke(gameInteractable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsValidInteractable(other.gameObject, out var gameInteractable)) return;

            if (CurrentInteractable != null)
            {
                CurrentInteractable.OnEndInteraction -= OnCurrentInteractableEndInteractionHandler;
                CurrentInteractable = null;
            }

            OnInteractableOutOfRange?.Invoke(gameInteractable);
        }

        private void OnCurrentInteractableEndInteractionHandler()
        {
            if (CurrentInteractable.CanInteract) return;
            
            CurrentInteractable.OnEndInteraction -= OnCurrentInteractableEndInteractionHandler;
            OnInteractableOutOfRange?.Invoke(CurrentInteractable);
                
            CurrentInteractable = null;
        }

        private bool IsValidInteractable(GameObject target, out IGameInteractable gameInteractable)
        {
            gameInteractable = default;
            
            if (target.layer != LayerUtils.InteractableLayerIndex) return false;
            if (!target.TryGetComponent(out gameInteractable)) return false;
            if (!gameInteractable.CanInteract) return false;
            
            return true;
        }
    }
}