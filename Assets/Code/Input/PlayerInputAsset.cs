//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Code/Input/PlayerInputAsset.inputactions
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

namespace ARPG.Input
{
    public partial class @PlayerInputAsset : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputAsset()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAsset"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""08969cf6-7aeb-44c3-919e-159bc5ccebf8"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""b5742ca7-d879-495a-b944-ac53456b406c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""5c57c28c-7be9-465b-99d6-c6df4e7acf67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill0"",
                    ""type"": ""Button"",
                    ""id"": ""f7cab294-f4e0-42bd-8cb0-3fb28a3fd596"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill1"",
                    ""type"": ""Button"",
                    ""id"": ""a6939418-799c-4023-bf0b-64dba5f8ed99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill2"",
                    ""type"": ""Button"",
                    ""id"": ""940cd9fb-c19d-4348-8015-1b475c3ba2f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill3"",
                    ""type"": ""Button"",
                    ""id"": ""09432982-b422-4c83-9539-0ee417aab599"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill4"",
                    ""type"": ""Button"",
                    ""id"": ""5543956a-b1e5-4cac-9925-3a164fb2dfb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skill5"",
                    ""type"": ""Button"",
                    ""id"": ""43a03a68-fbba-4365-abc6-b8ef24a94d31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ForceStop"",
                    ""type"": ""Button"",
                    ""id"": ""eabd715e-1c6d-4c19-bda6-c67bb1d2ef23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""Value"",
                    ""id"": ""d5927181-8acb-42cb-ad7b-e12190b0c546"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""322506b4-f3f9-4f59-aaa5-d34e614bb334"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94630d4e-55f0-42c7-9ff5-36c04f4155f5"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""502a4d17-771e-4c6c-a07c-b7e11fc2bda8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39afbcfa-902f-4822-b539-8890dc4d383b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c061b42b-c3a0-481e-a423-f3a00f6d1028"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3e52146-3de2-47cd-8861-26911f0a7775"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccbc5ac5-aed6-4794-9597-8a5d39eaf665"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3a090ac-2599-4d83-810b-05a2fc885a06"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7dd10be4-39da-4b5a-a2b0-f71c0be9ae62"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""ForceStop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14e64f18-5058-4b8c-bd19-e1d2b2fc314e"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""ForceStop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59ba62aa-f850-484b-8245-0759dce840ad"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5ca3118-0bb4-4a00-8cb8-cbeaafeb2326"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d06d23e-45b8-4ca1-b46a-2b7033d6eedc"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec4d94b1-b432-4670-91f8-3f4c47ec0632"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""Skill5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13cc32f8-c355-4b2e-be9a-6b408d0f74d2"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Desktop"",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Desktop"",
            ""bindingGroup"": ""Desktop"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // PlayerInput
            m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
            m_PlayerInput_LeftClick = m_PlayerInput.FindAction("LeftClick", throwIfNotFound: true);
            m_PlayerInput_RightClick = m_PlayerInput.FindAction("RightClick", throwIfNotFound: true);
            m_PlayerInput_Skill0 = m_PlayerInput.FindAction("Skill0", throwIfNotFound: true);
            m_PlayerInput_Skill1 = m_PlayerInput.FindAction("Skill1", throwIfNotFound: true);
            m_PlayerInput_Skill2 = m_PlayerInput.FindAction("Skill2", throwIfNotFound: true);
            m_PlayerInput_Skill3 = m_PlayerInput.FindAction("Skill3", throwIfNotFound: true);
            m_PlayerInput_Skill4 = m_PlayerInput.FindAction("Skill4", throwIfNotFound: true);
            m_PlayerInput_Skill5 = m_PlayerInput.FindAction("Skill5", throwIfNotFound: true);
            m_PlayerInput_ForceStop = m_PlayerInput.FindAction("ForceStop", throwIfNotFound: true);
            m_PlayerInput_LeftStick = m_PlayerInput.FindAction("LeftStick", throwIfNotFound: true);
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

        // PlayerInput
        private readonly InputActionMap m_PlayerInput;
        private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
        private readonly InputAction m_PlayerInput_LeftClick;
        private readonly InputAction m_PlayerInput_RightClick;
        private readonly InputAction m_PlayerInput_Skill0;
        private readonly InputAction m_PlayerInput_Skill1;
        private readonly InputAction m_PlayerInput_Skill2;
        private readonly InputAction m_PlayerInput_Skill3;
        private readonly InputAction m_PlayerInput_Skill4;
        private readonly InputAction m_PlayerInput_Skill5;
        private readonly InputAction m_PlayerInput_ForceStop;
        private readonly InputAction m_PlayerInput_LeftStick;
        public struct PlayerInputActions
        {
            private @PlayerInputAsset m_Wrapper;
            public PlayerInputActions(@PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
            public InputAction @LeftClick => m_Wrapper.m_PlayerInput_LeftClick;
            public InputAction @RightClick => m_Wrapper.m_PlayerInput_RightClick;
            public InputAction @Skill0 => m_Wrapper.m_PlayerInput_Skill0;
            public InputAction @Skill1 => m_Wrapper.m_PlayerInput_Skill1;
            public InputAction @Skill2 => m_Wrapper.m_PlayerInput_Skill2;
            public InputAction @Skill3 => m_Wrapper.m_PlayerInput_Skill3;
            public InputAction @Skill4 => m_Wrapper.m_PlayerInput_Skill4;
            public InputAction @Skill5 => m_Wrapper.m_PlayerInput_Skill5;
            public InputAction @ForceStop => m_Wrapper.m_PlayerInput_ForceStop;
            public InputAction @LeftStick => m_Wrapper.m_PlayerInput_LeftStick;
            public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerInputActions instance)
            {
                if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
                {
                    @LeftClick.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftClick;
                    @RightClick.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRightClick;
                    @Skill0.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill0;
                    @Skill0.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill0;
                    @Skill0.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill0;
                    @Skill1.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill1;
                    @Skill1.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill1;
                    @Skill1.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill1;
                    @Skill2.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill2;
                    @Skill2.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill2;
                    @Skill2.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill2;
                    @Skill3.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill3;
                    @Skill3.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill3;
                    @Skill3.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill3;
                    @Skill4.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill4;
                    @Skill4.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill4;
                    @Skill4.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill4;
                    @Skill5.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill5;
                    @Skill5.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill5;
                    @Skill5.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnSkill5;
                    @ForceStop.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnForceStop;
                    @ForceStop.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnForceStop;
                    @ForceStop.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnForceStop;
                    @LeftStick.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftStick;
                    @LeftStick.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftStick;
                    @LeftStick.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLeftStick;
                }
                m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @Skill0.started += instance.OnSkill0;
                    @Skill0.performed += instance.OnSkill0;
                    @Skill0.canceled += instance.OnSkill0;
                    @Skill1.started += instance.OnSkill1;
                    @Skill1.performed += instance.OnSkill1;
                    @Skill1.canceled += instance.OnSkill1;
                    @Skill2.started += instance.OnSkill2;
                    @Skill2.performed += instance.OnSkill2;
                    @Skill2.canceled += instance.OnSkill2;
                    @Skill3.started += instance.OnSkill3;
                    @Skill3.performed += instance.OnSkill3;
                    @Skill3.canceled += instance.OnSkill3;
                    @Skill4.started += instance.OnSkill4;
                    @Skill4.performed += instance.OnSkill4;
                    @Skill4.canceled += instance.OnSkill4;
                    @Skill5.started += instance.OnSkill5;
                    @Skill5.performed += instance.OnSkill5;
                    @Skill5.canceled += instance.OnSkill5;
                    @ForceStop.started += instance.OnForceStop;
                    @ForceStop.performed += instance.OnForceStop;
                    @ForceStop.canceled += instance.OnForceStop;
                    @LeftStick.started += instance.OnLeftStick;
                    @LeftStick.performed += instance.OnLeftStick;
                    @LeftStick.canceled += instance.OnLeftStick;
                }
            }
        }
        public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
        private int m_DesktopSchemeIndex = -1;
        public InputControlScheme DesktopScheme
        {
            get
            {
                if (m_DesktopSchemeIndex == -1) m_DesktopSchemeIndex = asset.FindControlSchemeIndex("Desktop");
                return asset.controlSchemes[m_DesktopSchemeIndex];
            }
        }
        public interface IPlayerInputActions
        {
            void OnLeftClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnSkill0(InputAction.CallbackContext context);
            void OnSkill1(InputAction.CallbackContext context);
            void OnSkill2(InputAction.CallbackContext context);
            void OnSkill3(InputAction.CallbackContext context);
            void OnSkill4(InputAction.CallbackContext context);
            void OnSkill5(InputAction.CallbackContext context);
            void OnForceStop(InputAction.CallbackContext context);
            void OnLeftStick(InputAction.CallbackContext context);
        }
    }
}
