using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractableSystem.Controller;
using Gameplay.GameInteractableSystem.Data;
using Gameplay.GameInteractableSystem.Interfaces;
using Gameplay.UI.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityInteractor.UI
{
    public class PlayerInteractPanelUI : MonoBehaviour, IPlayerUI
    {
        [Header("Buttons")] 
        [SerializeField] private InteractionTipUI interactTip;

        private EntityGameInteractorController _playerInteractionFinderController;
        private bool _initiated;

        public void Initiate(IGameEntity playerEntity)
        {
            interactTip.Toggle(false);
            interactTip.ToggleSliderProgress(false);

            if (playerEntity.TryGetExtraComponent(out _playerInteractionFinderController))
            {
                _playerInteractionFinderController.OnGameInteractableAvailable += OnInteractableAvailableHandler;
                _playerInteractionFinderController.OnGameInteractableNotAvailable += OnInteractableNotAvailableHandler;
                _playerInteractionFinderController.OnStartTryingToInteract += OnPlayerStartInteractionHandler;
                _playerInteractionFinderController.OnCanceledTryingToInteract += OnPlayerCanceledInteractionHandler;
                _playerInteractionFinderController.OnStartInteraction += OnPlayerRequestedInteractionHandler;
            }
        }

        private void OnDestroy()
        {
            if (_playerInteractionFinderController != null)
            {
                _playerInteractionFinderController.OnGameInteractableAvailable -= OnInteractableAvailableHandler;
                _playerInteractionFinderController.OnGameInteractableNotAvailable -= OnInteractableNotAvailableHandler;
                _playerInteractionFinderController.OnStartTryingToInteract -= OnPlayerStartInteractionHandler;
                _playerInteractionFinderController.OnCanceledTryingToInteract -= OnPlayerCanceledInteractionHandler;
                _playerInteractionFinderController.OnStartInteraction -= OnPlayerRequestedInteractionHandler;
            }
        }

        public void ToggleUI(bool valueToSet)
        {
            gameObject.SetActive(valueToSet);
        }

        private void OnInteractableAvailableHandler(IGameInteractable gameInteractable)
        {
            interactTip.Toggle(true);
            interactTip.SetInteractionText(gameInteractable.InteractData.InteractInfoText);

            if (gameInteractable.InteractData.InteractType == InteractType.Hold)
                interactTip.ToggleSliderProgress(true);
        }

        private void OnInteractableNotAvailableHandler(IGameInteractable gameInteractable)
        {
            interactTip.Toggle(false);
            interactTip.ToggleSliderProgress(false);
        }

        private void OnPlayerStartInteractionHandler(IGameInteractable gameInteractable)
        {
            interactTip.StartInteractSliderAnimation(gameInteractable.InteractData.InteractionHoldDuration);
        }

        private void OnPlayerCanceledInteractionHandler(IGameInteractable gameInteractable)
        {
            interactTip.CancelInteractSliderAnimation();
        }

        private void OnPlayerRequestedInteractionHandler(IGameInteractable gameInteractable)
        {
            interactTip.CancelInteractSliderAnimation();
        }
    }
}