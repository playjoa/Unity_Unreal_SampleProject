using System;
using System.Collections;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityInteractor.Components;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractableSystem.Data;
using Gameplay.GameInteractableSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameInteractableSystem.Controller
{
    public class EntityGameInteractorController : BaseEntityExtraComponent
    {
        [Header("Interact Collider")]
        [SerializeField] private InteractionFinderCollider interactionFinder;
        
        public event Action<IGameInteractable> OnGameInteractableAvailable;
        public event Action<IGameInteractable> OnGameInteractableNotAvailable;
        
        public event Action<IGameInteractable> OnStartTryingToInteract;
        public event Action<IGameInteractable> OnCanceledTryingToInteract;
        public event Action<IGameInteractable> OnStartInteraction;

        public bool CanInteract => interactionFinder.CurrentInteractable != null &&
                                   interactionFinder.CurrentInteractable.CanInteract &&
                                   _currentInteractState == InteractState.Idle;

        private IGameInteractable _currentTryingToInteractInteractable;
        private IGameInteractable _currentInteractingInteractable;
        private Coroutine _tryingToInteractCoroutine;
        private InteractState _currentInteractState = InteractState.Idle;
        
        protected override void OnInitiate(IGameEntity owner)
        {
            interactionFinder.Initiate(this);

            owner.EntityBrain.OnEntityInteractRequest += OnEntityInteractRequestHandler; 
            owner.EntityBrain.OnEntityCanceledInteractRequest += OnEntityCanceledInteractRequestHandler; 
            interactionFinder.OnInteractableInRange += OnInteractableInRangeHandler;
            interactionFinder.OnInteractableOutOfRange += OnInteractableOutOfRangeHandler;
        }

        protected override void OnClean()
        {
            Owner.EntityBrain.OnEntityInteractRequest -= OnEntityInteractRequestHandler; 
            Owner.EntityBrain.OnEntityCanceledInteractRequest -= OnEntityCanceledInteractRequestHandler; 
            interactionFinder.OnInteractableInRange -= OnInteractableInRangeHandler;
            interactionFinder.OnInteractableOutOfRange -= OnInteractableOutOfRangeHandler;
        }

        private void Update()
        {
            if (_currentInteractState != InteractState.Interacting) return;
            if (_currentInteractingInteractable.InteractionUpdateTick()) return;
            
            _currentInteractingInteractable.EndInteraction();
            _currentInteractState = InteractState.Idle;
            _currentInteractingInteractable = null;
        }
        
        private IEnumerator TriggerInteractCoroutine(IGameInteractable targetInteractable)
        {
            _currentInteractState = InteractState.TryingToInteract;
            OnStartTryingToInteract?.Invoke(targetInteractable);
            
            if (targetInteractable.InteractData.InteractType == InteractType.Hold)
            {
                yield return new WaitForSeconds(targetInteractable.InteractData.InteractionHoldDuration);
            }

            StartInteraction(targetInteractable);
            _tryingToInteractCoroutine = null;
        }

        private void StartInteraction(IGameInteractable gameInteractable)
        {
            if (gameInteractable.TryStartInteraction(Owner))
            {
                _currentInteractingInteractable = gameInteractable;
                OnStartInteraction?.Invoke(_currentInteractingInteractable);
                _currentInteractState = InteractState.Interacting;
            }
            else
            {
                _currentInteractState = InteractState.Idle;
                _currentInteractingInteractable = null;
            }
        }

        private void OnEntityInteractRequestHandler()
        {
            if (!CanInteract) return;

            _currentTryingToInteractInteractable = interactionFinder.CurrentInteractable;
            _tryingToInteractCoroutine = StartCoroutine(TriggerInteractCoroutine(_currentTryingToInteractInteractable));
        }

        private void OnEntityCanceledInteractRequestHandler()
        {
            if (_tryingToInteractCoroutine == null) return;
            
            StopCoroutine(_tryingToInteractCoroutine);
            _tryingToInteractCoroutine = null;
            _currentInteractState = InteractState.Idle;
            OnCanceledTryingToInteract?.Invoke(_currentTryingToInteractInteractable);
        }

        private void OnInteractableInRangeHandler(IGameInteractable gameInteractable)
        {
            OnGameInteractableAvailable?.Invoke(gameInteractable);
        }

        private void OnInteractableOutOfRangeHandler(IGameInteractable gameInteractable)
        {
            OnGameInteractableNotAvailable?.Invoke(gameInteractable);
        }
    }
}