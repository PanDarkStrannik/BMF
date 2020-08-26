// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""MovementInput"",
            ""id"": ""2b6bd468-282a-4fc0-9a00-a374abfdd941"",
            ""actions"": [
                {
                    ""name"": ""GetDirection"",
                    ""type"": ""Value"",
                    ""id"": ""ccbc5f4e-c9f9-4dfa-91dd-09d58bb0dc68"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f144a53d-5206-4066-a639-ed7bf2bc5adf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetDirection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f145886c-0602-43b8-a160-d92654ab3f74"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""GetDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f56ffb5d-c294-4711-abb7-e6593704713d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""Invert,Clamp(max=1)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""GetDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""19e92a23-796b-4dee-a530-07b9f0a6ee33"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": ""Invert,Clamp(max=1)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""GetDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7205d46c-55f4-431c-af9d-292d041b56c4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""GetDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""RotationInput"",
            ""id"": ""47823a55-9c73-400b-8dcd-f451a1d1045c"",
            ""actions"": [
                {
                    ""name"": ""GetRotation"",
                    ""type"": ""Value"",
                    ""id"": ""1482543b-bab0-4886-b875-030da9d106a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""26c8e5dc-00dc-4507-9bda-a9e7d3a3e05b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetRotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a52575f8-3c5d-4d99-983a-28738c76d2ab"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2d4fee5a-3428-4924-8df0-fe594b9e916b"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""GetRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3b1afb3d-9a99-4564-b2d0-a06c8232511e"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""GetRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b349abdf-764e-4eed-8c36-8c55f13ae546"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""ButtonInputs"",
            ""id"": ""7e21ab00-bdba-48eb-871d-4deb5abf5877"",
            ""actions"": [
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""d454fa5c-8eea-4eaa-92e4-3f2b91614e60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""bc458cfe-e4ec-4eb0-80a7-e03bb64078e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""c2ea2e8d-6a2a-4e99-9376-ce6478f5e7a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heal"",
                    ""type"": ""Button"",
                    ""id"": ""cb226fdd-4f2f-4c63-9047-5b82e0c600ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""17c19c88-728e-4736-8029-6166c96a42a4"",
                    ""expectedControlType"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""6a45f470-39ee-40b8-bcd8-3bada907721b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Blink"",
                    ""type"": ""Button"",
                    ""id"": ""262ca9b5-09a6-45b5-8b0e-305f73b60dc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f22215fc-8137-4e0c-8c88-365806612a2c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0c73c44-05b3-4316-b7a5-52f23d8ec975"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aeae8199-5d54-43e4-921f-be021a302937"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9254611-3d50-48dc-826d-e70bd9371c4c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62957fad-a3da-4f7b-a3c9-9a413ccc209a"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""491486a2-1d60-4a73-b056-c9ccfbc8546c"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d6ce058-c58e-49ca-a7ee-6817959dbe66"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d207e94-7b84-45c6-ac15-f26391360e6f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6060e051-a22b-47f2-90cb-66b07c3d3c97"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""Blink"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse And Keybord"",
            ""bindingGroup"": ""Mouse And Keybord"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MovementInput
        m_MovementInput = asset.FindActionMap("MovementInput", throwIfNotFound: true);
        m_MovementInput_GetDirection = m_MovementInput.FindAction("GetDirection", throwIfNotFound: true);
        // RotationInput
        m_RotationInput = asset.FindActionMap("RotationInput", throwIfNotFound: true);
        m_RotationInput_GetRotation = m_RotationInput.FindAction("GetRotation", throwIfNotFound: true);
        // ButtonInputs
        m_ButtonInputs = asset.FindActionMap("ButtonInputs", throwIfNotFound: true);
        m_ButtonInputs_Reload = m_ButtonInputs.FindAction("Reload", throwIfNotFound: true);
        m_ButtonInputs_Shoot = m_ButtonInputs.FindAction("Shoot", throwIfNotFound: true);
        m_ButtonInputs_ChangeSpeed = m_ButtonInputs.FindAction("ChangeSpeed", throwIfNotFound: true);
        m_ButtonInputs_Heal = m_ButtonInputs.FindAction("Heal", throwIfNotFound: true);
        m_ButtonInputs_ChangeWeapon = m_ButtonInputs.FindAction("ChangeWeapon", throwIfNotFound: true);
        m_ButtonInputs_Jump = m_ButtonInputs.FindAction("Jump", throwIfNotFound: true);
        m_ButtonInputs_Blink = m_ButtonInputs.FindAction("Blink", throwIfNotFound: true);
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

    // MovementInput
    private readonly InputActionMap m_MovementInput;
    private IMovementInputActions m_MovementInputActionsCallbackInterface;
    private readonly InputAction m_MovementInput_GetDirection;
    public struct MovementInputActions
    {
        private @PlayerInput m_Wrapper;
        public MovementInputActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @GetDirection => m_Wrapper.m_MovementInput_GetDirection;
        public InputActionMap Get() { return m_Wrapper.m_MovementInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementInputActions set) { return set.Get(); }
        public void SetCallbacks(IMovementInputActions instance)
        {
            if (m_Wrapper.m_MovementInputActionsCallbackInterface != null)
            {
                @GetDirection.started -= m_Wrapper.m_MovementInputActionsCallbackInterface.OnGetDirection;
                @GetDirection.performed -= m_Wrapper.m_MovementInputActionsCallbackInterface.OnGetDirection;
                @GetDirection.canceled -= m_Wrapper.m_MovementInputActionsCallbackInterface.OnGetDirection;
            }
            m_Wrapper.m_MovementInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GetDirection.started += instance.OnGetDirection;
                @GetDirection.performed += instance.OnGetDirection;
                @GetDirection.canceled += instance.OnGetDirection;
            }
        }
    }
    public MovementInputActions @MovementInput => new MovementInputActions(this);

    // RotationInput
    private readonly InputActionMap m_RotationInput;
    private IRotationInputActions m_RotationInputActionsCallbackInterface;
    private readonly InputAction m_RotationInput_GetRotation;
    public struct RotationInputActions
    {
        private @PlayerInput m_Wrapper;
        public RotationInputActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @GetRotation => m_Wrapper.m_RotationInput_GetRotation;
        public InputActionMap Get() { return m_Wrapper.m_RotationInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RotationInputActions set) { return set.Get(); }
        public void SetCallbacks(IRotationInputActions instance)
        {
            if (m_Wrapper.m_RotationInputActionsCallbackInterface != null)
            {
                @GetRotation.started -= m_Wrapper.m_RotationInputActionsCallbackInterface.OnGetRotation;
                @GetRotation.performed -= m_Wrapper.m_RotationInputActionsCallbackInterface.OnGetRotation;
                @GetRotation.canceled -= m_Wrapper.m_RotationInputActionsCallbackInterface.OnGetRotation;
            }
            m_Wrapper.m_RotationInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GetRotation.started += instance.OnGetRotation;
                @GetRotation.performed += instance.OnGetRotation;
                @GetRotation.canceled += instance.OnGetRotation;
            }
        }
    }
    public RotationInputActions @RotationInput => new RotationInputActions(this);

    // ButtonInputs
    private readonly InputActionMap m_ButtonInputs;
    private IButtonInputsActions m_ButtonInputsActionsCallbackInterface;
    private readonly InputAction m_ButtonInputs_Reload;
    private readonly InputAction m_ButtonInputs_Shoot;
    private readonly InputAction m_ButtonInputs_ChangeSpeed;
    private readonly InputAction m_ButtonInputs_Heal;
    private readonly InputAction m_ButtonInputs_ChangeWeapon;
    private readonly InputAction m_ButtonInputs_Jump;
    private readonly InputAction m_ButtonInputs_Blink;
    public struct ButtonInputsActions
    {
        private @PlayerInput m_Wrapper;
        public ButtonInputsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Reload => m_Wrapper.m_ButtonInputs_Reload;
        public InputAction @Shoot => m_Wrapper.m_ButtonInputs_Shoot;
        public InputAction @ChangeSpeed => m_Wrapper.m_ButtonInputs_ChangeSpeed;
        public InputAction @Heal => m_Wrapper.m_ButtonInputs_Heal;
        public InputAction @ChangeWeapon => m_Wrapper.m_ButtonInputs_ChangeWeapon;
        public InputAction @Jump => m_Wrapper.m_ButtonInputs_Jump;
        public InputAction @Blink => m_Wrapper.m_ButtonInputs_Blink;
        public InputActionMap Get() { return m_Wrapper.m_ButtonInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ButtonInputsActions set) { return set.Get(); }
        public void SetCallbacks(IButtonInputsActions instance)
        {
            if (m_Wrapper.m_ButtonInputsActionsCallbackInterface != null)
            {
                @Reload.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnReload;
                @Shoot.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnShoot;
                @ChangeSpeed.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeSpeed;
                @ChangeSpeed.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeSpeed;
                @ChangeSpeed.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeSpeed;
                @Heal.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnHeal;
                @Heal.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnHeal;
                @Heal.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnHeal;
                @ChangeWeapon.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeWeapon;
                @ChangeWeapon.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeWeapon;
                @ChangeWeapon.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeWeapon;
                @Jump.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnJump;
                @Blink.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnBlink;
                @Blink.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnBlink;
                @Blink.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnBlink;
            }
            m_Wrapper.m_ButtonInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ChangeSpeed.started += instance.OnChangeSpeed;
                @ChangeSpeed.performed += instance.OnChangeSpeed;
                @ChangeSpeed.canceled += instance.OnChangeSpeed;
                @Heal.started += instance.OnHeal;
                @Heal.performed += instance.OnHeal;
                @Heal.canceled += instance.OnHeal;
                @ChangeWeapon.started += instance.OnChangeWeapon;
                @ChangeWeapon.performed += instance.OnChangeWeapon;
                @ChangeWeapon.canceled += instance.OnChangeWeapon;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Blink.started += instance.OnBlink;
                @Blink.performed += instance.OnBlink;
                @Blink.canceled += instance.OnBlink;
            }
        }
    }
    public ButtonInputsActions @ButtonInputs => new ButtonInputsActions(this);
    private int m_MouseAndKeybordSchemeIndex = -1;
    public InputControlScheme MouseAndKeybordScheme
    {
        get
        {
            if (m_MouseAndKeybordSchemeIndex == -1) m_MouseAndKeybordSchemeIndex = asset.FindControlSchemeIndex("Mouse And Keybord");
            return asset.controlSchemes[m_MouseAndKeybordSchemeIndex];
        }
    }
    public interface IMovementInputActions
    {
        void OnGetDirection(InputAction.CallbackContext context);
    }
    public interface IRotationInputActions
    {
        void OnGetRotation(InputAction.CallbackContext context);
    }
    public interface IButtonInputsActions
    {
        void OnReload(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnChangeSpeed(InputAction.CallbackContext context);
        void OnHeal(InputAction.CallbackContext context);
        void OnChangeWeapon(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBlink(InputAction.CallbackContext context);
    }
}
