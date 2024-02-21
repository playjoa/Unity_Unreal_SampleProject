using System;
using Gameplay.GameInteractable.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.Components
{
    public class InteractableFinderCollider : MonoBehaviour
    {
        public event Action<IGameInteractable> OnTriggerEnterInteractable;
        public event Action<IGameInteractable> OnTriggerExitInteractable;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IGameInteractable>(out var interactable)) return;
            if (!interactable.CanInteract) return;
            
            OnTriggerEnterInteractable?.Invoke(interactable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IGameInteractable>(out var interactable)) return;
            if (!interactable.CanInteract) return;

            OnTriggerExitInteractable?.Invoke(interactable);
        }
    }
}