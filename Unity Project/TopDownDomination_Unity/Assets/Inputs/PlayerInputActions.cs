//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerGameplayInputs"",
            ""id"": ""7e35cc40-5369-44da-8e95-35f872cc223f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b853926a-147e-4b37-a984-51f32ff9912a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FirePrimary"",
                    ""type"": ""Button"",
                    ""id"": ""ffb33295-a1cb-47bc-835b-a4a53838641a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FireSecondary"",
                    ""type"": ""Button"",
                    ""id"": ""5a80780f-9257-4dcf-affa-1a21303e4737"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7a61cf74-bf82-4b04-97cf-8a3fa5dcb107"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""abfe0b67-31af-40f5-82c5-65ce13fac917"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""28e38a65-9880-444c-a574-ec4488da6272"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""394d2a38-8ae9-44c8-9103-5c7d55ab30bc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8a14e5b8-aa87-4e63-a05a-5969400e2985"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d8ef483a-6fe3-4f2f-b719-1e1880d49fbe"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""56e56473-5a68-4429-9f9c-b4c71031b140"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ab94178a-6304-4b62-a2c0-16f99245de07"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""FirePrimary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""146c4df7-6903-42eb-aff6-ea4c7b6001de"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""FireSecondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6faf5964-d2c2-40b9-bf4d-8cd8c0359d40"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""871fc98c-19fc-4b3b-9f55-e5be88633191"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerInputs"",
            ""bindingGroup"": ""PlayerInputs"",
            ""devices"": []
        }
    ]
}");
        // PlayerGameplayInputs
        m_PlayerGameplayInputs = asset.FindActionMap("PlayerGameplayInputs", throwIfNotFound: true);
        m_PlayerGameplayInputs_Movement = m_PlayerGameplayInputs.FindAction("Movement", throwIfNotFound: true);
        m_PlayerGameplayInputs_FirePrimary = m_PlayerGameplayInputs.FindAction("FirePrimary", throwIfNotFound: true);
        m_PlayerGameplayInputs_FireSecondary = m_PlayerGameplayInputs.FindAction("FireSecondary", throwIfNotFound: true);
        m_PlayerGameplayInputs_Interact = m_PlayerGameplayInputs.FindAction("Interact", throwIfNotFound: true);
        m_PlayerGameplayInputs_Aim = m_PlayerGameplayInputs.FindAction("Aim", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerGameplayInputs
    private readonly InputActionMap m_PlayerGameplayInputs;
    private List<IPlayerGameplayInputsActions> m_PlayerGameplayInputsActionsCallbackInterfaces = new List<IPlayerGameplayInputsActions>();
    private readonly InputAction m_PlayerGameplayInputs_Movement;
    private readonly InputAction m_PlayerGameplayInputs_FirePrimary;
    private readonly InputAction m_PlayerGameplayInputs_FireSecondary;
    private readonly InputAction m_PlayerGameplayInputs_Interact;
    private readonly InputAction m_PlayerGameplayInputs_Aim;
    public struct PlayerGameplayInputsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerGameplayInputsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerGameplayInputs_Movement;
        public InputAction @FirePrimary => m_Wrapper.m_PlayerGameplayInputs_FirePrimary;
        public InputAction @FireSecondary => m_Wrapper.m_PlayerGameplayInputs_FireSecondary;
        public InputAction @Interact => m_Wrapper.m_PlayerGameplayInputs_Interact;
        public InputAction @Aim => m_Wrapper.m_PlayerGameplayInputs_Aim;
        public InputActionMap Get() { return m_Wrapper.m_PlayerGameplayInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerGameplayInputsActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerGameplayInputsActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerGameplayInputsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerGameplayInputsActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @FirePrimary.started += instance.OnFirePrimary;
            @FirePrimary.performed += instance.OnFirePrimary;
            @FirePrimary.canceled += instance.OnFirePrimary;
            @FireSecondary.started += instance.OnFireSecondary;
            @FireSecondary.performed += instance.OnFireSecondary;
            @FireSecondary.canceled += instance.OnFireSecondary;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
        }

        private void UnregisterCallbacks(IPlayerGameplayInputsActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @FirePrimary.started -= instance.OnFirePrimary;
            @FirePrimary.performed -= instance.OnFirePrimary;
            @FirePrimary.canceled -= instance.OnFirePrimary;
            @FireSecondary.started -= instance.OnFireSecondary;
            @FireSecondary.performed -= instance.OnFireSecondary;
            @FireSecondary.canceled -= instance.OnFireSecondary;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
        }

        public void RemoveCallbacks(IPlayerGameplayInputsActions instance)
        {
            if (m_Wrapper.m_PlayerGameplayInputsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerGameplayInputsActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerGameplayInputsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerGameplayInputsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerGameplayInputsActions @PlayerGameplayInputs => new PlayerGameplayInputsActions(this);
    private int m_PlayerInputsSchemeIndex = -1;
    public InputControlScheme PlayerInputsScheme
    {
        get
        {
            if (m_PlayerInputsSchemeIndex == -1) m_PlayerInputsSchemeIndex = asset.FindControlSchemeIndex("PlayerInputs");
            return asset.controlSchemes[m_PlayerInputsSchemeIndex];
        }
    }
    public interface IPlayerGameplayInputsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFirePrimary(InputAction.CallbackContext context);
        void OnFireSecondary(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}