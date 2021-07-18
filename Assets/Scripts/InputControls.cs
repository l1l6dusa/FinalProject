using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""941c9ad4-a101-4485-baed-ed9162ff7635"",
            ""actions"": [
                {
                    ""name"": ""Direction"",
                    ""type"": ""Value"",
                    ""id"": ""5748e83b-d888-49d0-b9b6-92f197a967a7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""12c926ea-116c-4022-a619-044e6b965d47"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShowPointer"",
                    ""type"": ""Button"",
                    ""id"": ""41deab5f-be86-4cb3-bee8-e362965d2e7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Click And Drag Binding"",
                    ""id"": ""76fe3e42-5c93-432c-8305-5128f480f3e1"",
                    ""path"": ""ClickAndDragBinding"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MouseButton"",
                    ""id"": ""04be22b1-6d0d-46e7-bfba-50e3a675580a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""MouseVector"",
                    ""id"": ""97f22356-6a56-407a-9c9b-25d5bd9018b1"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""eada92b0-3c14-4140-85b7-d378c11049db"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8523f2f-b8ee-49c7-b06e-5aad222575ec"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""ShowPointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Direction = m_Main.FindAction("Direction", throwIfNotFound: true);
        m_Main_Shoot = m_Main.FindAction("Shoot", throwIfNotFound: true);
        m_Main_ShowPointer = m_Main.FindAction("ShowPointer", throwIfNotFound: true);
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

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Direction;
    private readonly InputAction m_Main_Shoot;
    private readonly InputAction m_Main_ShowPointer;
    public struct MainActions
    {
        private @InputControls m_Wrapper;
        public MainActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Direction => m_Wrapper.m_Main_Direction;
        public InputAction @Shoot => m_Wrapper.m_Main_Shoot;
        public InputAction @ShowPointer => m_Wrapper.m_Main_ShowPointer;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Direction.started -= m_Wrapper.m_MainActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnDirection;
                @Shoot.started -= m_Wrapper.m_MainActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnShoot;
                @ShowPointer.started -= m_Wrapper.m_MainActionsCallbackInterface.OnShowPointer;
                @ShowPointer.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnShowPointer;
                @ShowPointer.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnShowPointer;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ShowPointer.started += instance.OnShowPointer;
                @ShowPointer.performed += instance.OnShowPointer;
                @ShowPointer.canceled += instance.OnShowPointer;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IMainActions
    {
        void OnDirection(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnShowPointer(InputAction.CallbackContext context);
    }
}
