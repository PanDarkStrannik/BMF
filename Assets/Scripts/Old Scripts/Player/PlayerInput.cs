// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Old Scripts/Player/PlayerInput.inputactions'

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
                },
                {
                    ""name"": ""VectorUp(temp)"",
                    ""type"": ""Value"",
                    ""id"": ""a2513fc2-10dd-4e41-9c7c-2afa686e1359"",
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
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3a3ff48a-e0b7-4277-9cc6-926c904b8197"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VectorUp(temp)"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""10a9940f-886f-4ce9-8c46-acbba919be75"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""VectorUp(temp)"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fea8b569-9a21-46fe-b994-452ee6fcf495"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""VectorUp(temp)"",
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
                    ""path"": ""2DVector(mode=2)"",
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
                    ""name"": ""MainAttack"",
                    ""type"": ""Button"",
                    ""id"": ""bc458cfe-e4ec-4eb0-80a7-e03bb64078e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondAttack"",
                    ""type"": ""Button"",
                    ""id"": ""28f7f73c-0b3d-48d5-8f61-e426a14ae936"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""d454fa5c-8eea-4eaa-92e4-3f2b91614e60"",
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
                    ""name"": ""ChangeWeaponByKeyboard"",
                    ""type"": ""Value"",
                    ""id"": ""17c19c88-728e-4736-8029-6166c96a42a4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
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
                },
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""693c5d5f-6dd9-4bb4-9ff6-52c83a259f48"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""Value"",
                    ""id"": ""eecf0386-e165-438c-819b-3c3df2e664e3"",
                    ""expectedControlType"": """",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
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
                    ""action"": ""ChangeWeaponByKeyboard"",
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
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f33270f-2104-48a1-b494-ec3fea795455"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=2)"",
                    ""groups"": """",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6dd07ace-8b2c-4141-9144-a4066467180d"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a23f0003-d225-4d02-89ba-fd315652a8d3"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3d77383-b7db-428b-9834-6d33764e5559"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=5)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b3099b6-59c2-4b2e-a1b2-471e675f9731"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=6)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bf0c8f6-b944-470b-a2c5-832338dba890"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=7)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7c3b94b-90f9-4686-bc2d-c43677e689b1"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=8)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""619bffed-efe7-4cc2-b05c-a480822ca122"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=9)"",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ChangeWeaponByKeyboard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d207e94-7b84-45c6-ac15-f26391360e6f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""ecebe920-bb85-4276-8c21-4975cde34833"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""ESC"",
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
                    ""name"": ""1D Axis"",
                    ""id"": ""4f30a999-e2b4-476c-aba0-57d4cbf25e14"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a21a2013-0d74-43de-bad4-a07cec73d458"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1c39e659-16d3-492d-ba3b-f9a126564e89"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8e3c3ae8-f538-4306-a6c6-30953a5f8910"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""SecondAttack"",
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
                    ""action"": ""MainAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
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
                }
            ]
        },
        {
            ""name"": ""Interaction(For Teaser)"",
            ""id"": ""ca2c7f20-02ab-476a-9df0-fc2e02aab928"",
            ""actions"": [
                {
                    ""name"": ""TurnOn Animation"",
                    ""type"": ""Button"",
                    ""id"": ""874e6c6e-fe4d-4523-8307-07f365e114fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""64660c8c-996c-4356-9f61-d36320e19518"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse And Keybord"",
                    ""action"": ""TurnOn Animation"",
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
        m_MovementInput_VectorUptemp = m_MovementInput.FindAction("VectorUp(temp)", throwIfNotFound: true);
        // RotationInput
        m_RotationInput = asset.FindActionMap("RotationInput", throwIfNotFound: true);
        m_RotationInput_GetRotation = m_RotationInput.FindAction("GetRotation", throwIfNotFound: true);
        // ButtonInputs
        m_ButtonInputs = asset.FindActionMap("ButtonInputs", throwIfNotFound: true);
        m_ButtonInputs_MainAttack = m_ButtonInputs.FindAction("MainAttack", throwIfNotFound: true);
        m_ButtonInputs_SecondAttack = m_ButtonInputs.FindAction("SecondAttack", throwIfNotFound: true);
        m_ButtonInputs_Reload = m_ButtonInputs.FindAction("Reload", throwIfNotFound: true);
        m_ButtonInputs_ChangeSpeed = m_ButtonInputs.FindAction("ChangeSpeed", throwIfNotFound: true);
        m_ButtonInputs_Heal = m_ButtonInputs.FindAction("Heal", throwIfNotFound: true);
        m_ButtonInputs_ChangeWeaponByKeyboard = m_ButtonInputs.FindAction("ChangeWeaponByKeyboard", throwIfNotFound: true);
        m_ButtonInputs_Jump = m_ButtonInputs.FindAction("Jump", throwIfNotFound: true);
        m_ButtonInputs_Blink = m_ButtonInputs.FindAction("Blink", throwIfNotFound: true);
        m_ButtonInputs_ESC = m_ButtonInputs.FindAction("ESC", throwIfNotFound: true);
        m_ButtonInputs_MouseScroll = m_ButtonInputs.FindAction("MouseScroll", throwIfNotFound: true);
        // Interaction(For Teaser)
        m_InteractionForTeaser = asset.FindActionMap("Interaction(For Teaser)", throwIfNotFound: true);
        m_InteractionForTeaser_TurnOnAnimation = m_InteractionForTeaser.FindAction("TurnOn Animation", throwIfNotFound: true);
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
    private readonly InputAction m_MovementInput_VectorUptemp;
    public struct MovementInputActions
    {
        private @PlayerInput m_Wrapper;
        public MovementInputActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @GetDirection => m_Wrapper.m_MovementInput_GetDirection;
        public InputAction @VectorUptemp => m_Wrapper.m_MovementInput_VectorUptemp;
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
                @VectorUptemp.started -= m_Wrapper.m_MovementInputActionsCallbackInterface.OnVectorUptemp;
                @VectorUptemp.performed -= m_Wrapper.m_MovementInputActionsCallbackInterface.OnVectorUptemp;
                @VectorUptemp.canceled -= m_Wrapper.m_MovementInputActionsCallbackInterface.OnVectorUptemp;
            }
            m_Wrapper.m_MovementInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GetDirection.started += instance.OnGetDirection;
                @GetDirection.performed += instance.OnGetDirection;
                @GetDirection.canceled += instance.OnGetDirection;
                @VectorUptemp.started += instance.OnVectorUptemp;
                @VectorUptemp.performed += instance.OnVectorUptemp;
                @VectorUptemp.canceled += instance.OnVectorUptemp;
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
    private readonly InputAction m_ButtonInputs_MainAttack;
    private readonly InputAction m_ButtonInputs_SecondAttack;
    private readonly InputAction m_ButtonInputs_Reload;
    private readonly InputAction m_ButtonInputs_ChangeSpeed;
    private readonly InputAction m_ButtonInputs_Heal;
    private readonly InputAction m_ButtonInputs_ChangeWeaponByKeyboard;
    private readonly InputAction m_ButtonInputs_Jump;
    private readonly InputAction m_ButtonInputs_Blink;
    private readonly InputAction m_ButtonInputs_ESC;
    private readonly InputAction m_ButtonInputs_MouseScroll;
    public struct ButtonInputsActions
    {
        private @PlayerInput m_Wrapper;
        public ButtonInputsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MainAttack => m_Wrapper.m_ButtonInputs_MainAttack;
        public InputAction @SecondAttack => m_Wrapper.m_ButtonInputs_SecondAttack;
        public InputAction @Reload => m_Wrapper.m_ButtonInputs_Reload;
        public InputAction @ChangeSpeed => m_Wrapper.m_ButtonInputs_ChangeSpeed;
        public InputAction @Heal => m_Wrapper.m_ButtonInputs_Heal;
        public InputAction @ChangeWeaponByKeyboard => m_Wrapper.m_ButtonInputs_ChangeWeaponByKeyboard;
        public InputAction @Jump => m_Wrapper.m_ButtonInputs_Jump;
        public InputAction @Blink => m_Wrapper.m_ButtonInputs_Blink;
        public InputAction @ESC => m_Wrapper.m_ButtonInputs_ESC;
        public InputAction @MouseScroll => m_Wrapper.m_ButtonInputs_MouseScroll;
        public InputActionMap Get() { return m_Wrapper.m_ButtonInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ButtonInputsActions set) { return set.Get(); }
        public void SetCallbacks(IButtonInputsActions instance)
        {
            if (m_Wrapper.m_ButtonInputsActionsCallbackInterface != null)
            {
                @MainAttack.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnMainAttack;
                @MainAttack.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnMainAttack;
                @MainAttack.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnMainAttack;
                @SecondAttack.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnSecondAttack;
                @SecondAttack.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnSecondAttack;
                @SecondAttack.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnSecondAttack;
                @Reload.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnReload;
                @ChangeSpeed.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeSpeed;
                @ChangeSpeed.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeSpeed;
                @ChangeSpeed.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeSpeed;
                @Heal.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnHeal;
                @Heal.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnHeal;
                @Heal.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnHeal;
                @ChangeWeaponByKeyboard.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeWeaponByKeyboard;
                @ChangeWeaponByKeyboard.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeWeaponByKeyboard;
                @ChangeWeaponByKeyboard.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnChangeWeaponByKeyboard;
                @Jump.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnJump;
                @Blink.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnBlink;
                @Blink.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnBlink;
                @Blink.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnBlink;
                @ESC.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnESC;
                @MouseScroll.started -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.performed -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.canceled -= m_Wrapper.m_ButtonInputsActionsCallbackInterface.OnMouseScroll;
            }
            m_Wrapper.m_ButtonInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MainAttack.started += instance.OnMainAttack;
                @MainAttack.performed += instance.OnMainAttack;
                @MainAttack.canceled += instance.OnMainAttack;
                @SecondAttack.started += instance.OnSecondAttack;
                @SecondAttack.performed += instance.OnSecondAttack;
                @SecondAttack.canceled += instance.OnSecondAttack;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @ChangeSpeed.started += instance.OnChangeSpeed;
                @ChangeSpeed.performed += instance.OnChangeSpeed;
                @ChangeSpeed.canceled += instance.OnChangeSpeed;
                @Heal.started += instance.OnHeal;
                @Heal.performed += instance.OnHeal;
                @Heal.canceled += instance.OnHeal;
                @ChangeWeaponByKeyboard.started += instance.OnChangeWeaponByKeyboard;
                @ChangeWeaponByKeyboard.performed += instance.OnChangeWeaponByKeyboard;
                @ChangeWeaponByKeyboard.canceled += instance.OnChangeWeaponByKeyboard;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Blink.started += instance.OnBlink;
                @Blink.performed += instance.OnBlink;
                @Blink.canceled += instance.OnBlink;
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
                @MouseScroll.started += instance.OnMouseScroll;
                @MouseScroll.performed += instance.OnMouseScroll;
                @MouseScroll.canceled += instance.OnMouseScroll;
            }
        }
    }
    public ButtonInputsActions @ButtonInputs => new ButtonInputsActions(this);

    // Interaction(For Teaser)
    private readonly InputActionMap m_InteractionForTeaser;
    private IInteractionForTeaserActions m_InteractionForTeaserActionsCallbackInterface;
    private readonly InputAction m_InteractionForTeaser_TurnOnAnimation;
    public struct InteractionForTeaserActions
    {
        private @PlayerInput m_Wrapper;
        public InteractionForTeaserActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TurnOnAnimation => m_Wrapper.m_InteractionForTeaser_TurnOnAnimation;
        public InputActionMap Get() { return m_Wrapper.m_InteractionForTeaser; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionForTeaserActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionForTeaserActions instance)
        {
            if (m_Wrapper.m_InteractionForTeaserActionsCallbackInterface != null)
            {
                @TurnOnAnimation.started -= m_Wrapper.m_InteractionForTeaserActionsCallbackInterface.OnTurnOnAnimation;
                @TurnOnAnimation.performed -= m_Wrapper.m_InteractionForTeaserActionsCallbackInterface.OnTurnOnAnimation;
                @TurnOnAnimation.canceled -= m_Wrapper.m_InteractionForTeaserActionsCallbackInterface.OnTurnOnAnimation;
            }
            m_Wrapper.m_InteractionForTeaserActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TurnOnAnimation.started += instance.OnTurnOnAnimation;
                @TurnOnAnimation.performed += instance.OnTurnOnAnimation;
                @TurnOnAnimation.canceled += instance.OnTurnOnAnimation;
            }
        }
    }
    public InteractionForTeaserActions @InteractionForTeaser => new InteractionForTeaserActions(this);
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
        void OnVectorUptemp(InputAction.CallbackContext context);
    }
    public interface IRotationInputActions
    {
        void OnGetRotation(InputAction.CallbackContext context);
    }
    public interface IButtonInputsActions
    {
        void OnMainAttack(InputAction.CallbackContext context);
        void OnSecondAttack(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnChangeSpeed(InputAction.CallbackContext context);
        void OnHeal(InputAction.CallbackContext context);
        void OnChangeWeaponByKeyboard(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBlink(InputAction.CallbackContext context);
        void OnESC(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
    }
    public interface IInteractionForTeaserActions
    {
        void OnTurnOnAnimation(InputAction.CallbackContext context);
    }
}
