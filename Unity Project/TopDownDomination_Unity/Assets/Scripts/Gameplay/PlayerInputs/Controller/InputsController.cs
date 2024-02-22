using System;
using System.Collections;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.PlayerInputs.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.PlayerInputs.Controller
{
    public class InputsController : MonoBehaviour, IGameplaySystem
    {
        public event Action OnPrimaryFire;
        public event Action OnStartSecondaryFire;
        public event Action OnConfirmSecondaryFire;
        public event Action OnPlayerInteractPerformInput;
        public event Action OnPlayerInteractCanceledInput;
        
        public Vector2 PlayerMovementInput => _inputActions.Movement.ReadValue<Vector2>();
        public Vector2 PlayerAimInput => _inputActions.Aim.ReadValue<Vector2>();
        
        private PlayerInputActions _playerInputActions;
        private PlayerInputActions.PlayerGameplayInputsActions _inputActions;
        
        public IEnumerator Initiate(GameController gameController)
        {
            _playerInputActions = new PlayerInputActions();
            _inputActions = _playerInputActions.PlayerGameplayInputs;
            
            yield return true;
            
            _inputActions.FirePrimary.performed += InputFirePrimaryHandler;
            _inputActions.FireSecondary.started += InputStartFiringSecondaryHandler;
            _inputActions.FireSecondary.canceled += InputConfirmFireSecondaryHandler;
            _inputActions.Interact.started += InputInteractPerformHandler;
            _inputActions.Interact.canceled += InputInteractCanceledHandler;
            
            TogglePlayerInputs(true);
        }
        
        public void OnCleanUp()
        {
            _inputActions.FirePrimary.performed -= InputFirePrimaryHandler;
            _inputActions.FireSecondary.started -= InputStartFiringSecondaryHandler;
            _inputActions.FireSecondary.canceled -= InputConfirmFireSecondaryHandler;
            _inputActions.Interact.started -= InputInteractPerformHandler;
            _inputActions.Interact.canceled -= InputInteractCanceledHandler;
        }
        
        public void TogglePlayerInputs(bool valueToSet)
        {
            if (valueToSet == _playerInputActions.asset.enabled) return;
            
            if (valueToSet)
                _playerInputActions.Enable();
            else
                _playerInputActions.Disable();
        }
        
        private void InputFirePrimaryHandler(InputAction.CallbackContext context)
        {
            if (InputUtils.MouseOnTopOfUI()) return;
            
            OnPrimaryFire?.Invoke();
        }

        private void InputStartFiringSecondaryHandler(InputAction.CallbackContext context)
        {
            OnStartSecondaryFire?.Invoke();
        }
        
        private void InputConfirmFireSecondaryHandler(InputAction.CallbackContext context)
        {
            OnConfirmSecondaryFire?.Invoke();
        }
        
        private void InputInteractPerformHandler(InputAction.CallbackContext context)
        {
            OnPlayerInteractPerformInput?.Invoke();
        }
        
        
        private void InputInteractCanceledHandler(InputAction.CallbackContext context)
        {
            OnPlayerInteractCanceledInput?.Invoke();
        }

    }
}