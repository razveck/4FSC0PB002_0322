//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Tower Defense/Erik/TowerDefense_Input.inputactions
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

public partial class @TowerDefense_Input : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @TowerDefense_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TowerDefense_Input"",
    ""maps"": [
        {
            ""name"": ""GameControl"",
            ""id"": ""cf168872-8015-4df6-b5eb-9951f321979f"",
            ""actions"": [
                {
                    ""name"": ""PlaceTower"",
                    ""type"": ""Button"",
                    ""id"": ""64c907e9-814e-44bd-b2ac-fe8b01fb333d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""ed2e96ea-f828-4abd-9062-87595de38f47"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ee6b0c08-3fb6-433d-9443-51bc0502ba5e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""PlaceTower"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1a6c95d-14b8-4aed-849a-50bc69fdff03"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse and Keyboard"",
            ""bindingGroup"": ""Mouse and Keyboard"",
            ""devices"": []
        }
    ]
}");
        // GameControl
        m_GameControl = asset.FindActionMap("GameControl", throwIfNotFound: true);
        m_GameControl_PlaceTower = m_GameControl.FindAction("PlaceTower", throwIfNotFound: true);
        m_GameControl_MousePos = m_GameControl.FindAction("MousePos", throwIfNotFound: true);
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

    // GameControl
    private readonly InputActionMap m_GameControl;
    private IGameControlActions m_GameControlActionsCallbackInterface;
    private readonly InputAction m_GameControl_PlaceTower;
    private readonly InputAction m_GameControl_MousePos;
    public struct GameControlActions
    {
        private @TowerDefense_Input m_Wrapper;
        public GameControlActions(@TowerDefense_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceTower => m_Wrapper.m_GameControl_PlaceTower;
        public InputAction @MousePos => m_Wrapper.m_GameControl_MousePos;
        public InputActionMap Get() { return m_Wrapper.m_GameControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameControlActions set) { return set.Get(); }
        public void SetCallbacks(IGameControlActions instance)
        {
            if (m_Wrapper.m_GameControlActionsCallbackInterface != null)
            {
                @PlaceTower.started -= m_Wrapper.m_GameControlActionsCallbackInterface.OnPlaceTower;
                @PlaceTower.performed -= m_Wrapper.m_GameControlActionsCallbackInterface.OnPlaceTower;
                @PlaceTower.canceled -= m_Wrapper.m_GameControlActionsCallbackInterface.OnPlaceTower;
                @MousePos.started -= m_Wrapper.m_GameControlActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_GameControlActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_GameControlActionsCallbackInterface.OnMousePos;
            }
            m_Wrapper.m_GameControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlaceTower.started += instance.OnPlaceTower;
                @PlaceTower.performed += instance.OnPlaceTower;
                @PlaceTower.canceled += instance.OnPlaceTower;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
            }
        }
    }
    public GameControlActions @GameControl => new GameControlActions(this);
    private int m_MouseandKeyboardSchemeIndex = -1;
    public InputControlScheme MouseandKeyboardScheme
    {
        get
        {
            if (m_MouseandKeyboardSchemeIndex == -1) m_MouseandKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and Keyboard");
            return asset.controlSchemes[m_MouseandKeyboardSchemeIndex];
        }
    }
    public interface IGameControlActions
    {
        void OnPlaceTower(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
    }
}