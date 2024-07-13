//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/StandartInput.inputactions
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

public partial class @StandartInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @StandartInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""StandartInput"",
    ""maps"": [
        {
            ""name"": ""Standart"",
            ""id"": ""0bf5ded1-ba91-4899-87d0-6a5e8a5ca73a"",
            ""actions"": [
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b9e9656c-8a2c-4ce6-a0d9-4d8ea11f85e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""009f39e1-2218-465b-8832-8ba71c2fdd96"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""KayBoard"",
                    ""type"": ""Button"",
                    ""id"": ""10863f3f-2c17-411e-9c0c-711a31942d65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c32e72a0-edd0-487b-987f-0e3941e89623"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1c05683-e090-4dcf-91e1-3d916131e28f"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19e6cfce-1a33-4e55-ac23-50ac1d8bd0f2"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mobile"",
                    ""action"": ""KayBoard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Standart
        m_Standart = asset.FindActionMap("Standart", throwIfNotFound: true);
        m_Standart_PrimaryContact = m_Standart.FindAction("PrimaryContact", throwIfNotFound: true);
        m_Standart_PrimaryPosition = m_Standart.FindAction("PrimaryPosition", throwIfNotFound: true);
        m_Standart_KayBoard = m_Standart.FindAction("KayBoard", throwIfNotFound: true);
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

    // Standart
    private readonly InputActionMap m_Standart;
    private List<IStandartActions> m_StandartActionsCallbackInterfaces = new List<IStandartActions>();
    private readonly InputAction m_Standart_PrimaryContact;
    private readonly InputAction m_Standart_PrimaryPosition;
    private readonly InputAction m_Standart_KayBoard;
    public struct StandartActions
    {
        private @StandartInput m_Wrapper;
        public StandartActions(@StandartInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryContact => m_Wrapper.m_Standart_PrimaryContact;
        public InputAction @PrimaryPosition => m_Wrapper.m_Standart_PrimaryPosition;
        public InputAction @KayBoard => m_Wrapper.m_Standart_KayBoard;
        public InputActionMap Get() { return m_Wrapper.m_Standart; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StandartActions set) { return set.Get(); }
        public void AddCallbacks(IStandartActions instance)
        {
            if (instance == null || m_Wrapper.m_StandartActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_StandartActionsCallbackInterfaces.Add(instance);
            @PrimaryContact.started += instance.OnPrimaryContact;
            @PrimaryContact.performed += instance.OnPrimaryContact;
            @PrimaryContact.canceled += instance.OnPrimaryContact;
            @PrimaryPosition.started += instance.OnPrimaryPosition;
            @PrimaryPosition.performed += instance.OnPrimaryPosition;
            @PrimaryPosition.canceled += instance.OnPrimaryPosition;
            @KayBoard.started += instance.OnKayBoard;
            @KayBoard.performed += instance.OnKayBoard;
            @KayBoard.canceled += instance.OnKayBoard;
        }

        private void UnregisterCallbacks(IStandartActions instance)
        {
            @PrimaryContact.started -= instance.OnPrimaryContact;
            @PrimaryContact.performed -= instance.OnPrimaryContact;
            @PrimaryContact.canceled -= instance.OnPrimaryContact;
            @PrimaryPosition.started -= instance.OnPrimaryPosition;
            @PrimaryPosition.performed -= instance.OnPrimaryPosition;
            @PrimaryPosition.canceled -= instance.OnPrimaryPosition;
            @KayBoard.started -= instance.OnKayBoard;
            @KayBoard.performed -= instance.OnKayBoard;
            @KayBoard.canceled -= instance.OnKayBoard;
        }

        public void RemoveCallbacks(IStandartActions instance)
        {
            if (m_Wrapper.m_StandartActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IStandartActions instance)
        {
            foreach (var item in m_Wrapper.m_StandartActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_StandartActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public StandartActions @Standart => new StandartActions(this);
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IStandartActions
    {
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
        void OnKayBoard(InputAction.CallbackContext context);
    }
}
