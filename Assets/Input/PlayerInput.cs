//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from TopDown/Input/PlayerInput.inputactions
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

namespace TopDown
{
    public partial class @PlayerInput : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""31580196-a7ac-444f-aa36-d4c21bfc6784"",
            ""actions"": [
                {
                    ""name"": ""Direction"",
                    ""type"": ""Value"",
                    ""id"": ""8ccd2339-4eef-425d-ab00-76e5d0f6a0db"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""31efdf55-bcec-460f-9f97-a467e0c9d4cc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""249359cb-65e5-4978-87c4-9d8630dbf106"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f5502836-ffba-4a2b-b64c-03982cded4c8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b59b3a0e-d692-4f9f-99c4-49dbc56d0a58"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""095317fe-c003-4696-a900-448f9759cdbc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""1259a2c1-1bbf-4441-9064-b193a45e1237"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5c1a481d-915f-42ef-9402-7f79804da64c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cf850530-255b-4058-a40e-f02ca0c44079"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a209c801-3b45-4f0a-b2eb-22a31eac5467"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9b560159-59bd-4f76-bd7a-90c4a30824aa"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Rotation"",
            ""id"": ""bdc01eaa-7c26-4d7f-ad56-ca40f97f87a6"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""ff12d5a6-2e28-478c-87b6-dd29cb8c5e2b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""169a1809-811e-4bd3-a1dd-123e19ea1b2e"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Weapon"",
            ""id"": ""416adc2b-1c77-4275-83cb-88e2f67d6f27"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""779ddf96-1e99-4c24-bdd5-2b2aaa10c4d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7b27e753-e943-469a-8ded-caec1205b24b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bd7def8-839b-43de-b854-ee35b9714549"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
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
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_Direction = m_Movement.FindAction("Direction", throwIfNotFound: true);
            // Rotation
            m_Rotation = asset.FindActionMap("Rotation", throwIfNotFound: true);
            m_Rotation_Position = m_Rotation.FindAction("Position", throwIfNotFound: true);
            // Weapon
            m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
            m_Weapon_Shoot = m_Weapon.FindAction("Shoot", throwIfNotFound: true);
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

        // Movement
        private readonly InputActionMap m_Movement;
        private IMovementActions m_MovementActionsCallbackInterface;
        private readonly InputAction m_Movement_Direction;
        public struct MovementActions
        {
            private @PlayerInput m_Wrapper;
            public MovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Direction => m_Wrapper.m_Movement_Direction;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @Direction.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnDirection;
                    @Direction.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnDirection;
                    @Direction.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnDirection;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Direction.started += instance.OnDirection;
                    @Direction.performed += instance.OnDirection;
                    @Direction.canceled += instance.OnDirection;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // Rotation
        private readonly InputActionMap m_Rotation;
        private IRotationActions m_RotationActionsCallbackInterface;
        private readonly InputAction m_Rotation_Position;
        public struct RotationActions
        {
            private @PlayerInput m_Wrapper;
            public RotationActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Position => m_Wrapper.m_Rotation_Position;
            public InputActionMap Get() { return m_Wrapper.m_Rotation; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(RotationActions set) { return set.Get(); }
            public void SetCallbacks(IRotationActions instance)
            {
                if (m_Wrapper.m_RotationActionsCallbackInterface != null)
                {
                    @Position.started -= m_Wrapper.m_RotationActionsCallbackInterface.OnPosition;
                    @Position.performed -= m_Wrapper.m_RotationActionsCallbackInterface.OnPosition;
                    @Position.canceled -= m_Wrapper.m_RotationActionsCallbackInterface.OnPosition;
                }
                m_Wrapper.m_RotationActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Position.started += instance.OnPosition;
                    @Position.performed += instance.OnPosition;
                    @Position.canceled += instance.OnPosition;
                }
            }
        }
        public RotationActions @Rotation => new RotationActions(this);

        // Weapon
        private readonly InputActionMap m_Weapon;
        private IWeaponActions m_WeaponActionsCallbackInterface;
        private readonly InputAction m_Weapon_Shoot;
        public struct WeaponActions
        {
            private @PlayerInput m_Wrapper;
            public WeaponActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Shoot => m_Wrapper.m_Weapon_Shoot;
            public InputActionMap Get() { return m_Wrapper.m_Weapon; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
            public void SetCallbacks(IWeaponActions instance)
            {
                if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
                {
                    @Shoot.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnShoot;
                }
                m_Wrapper.m_WeaponActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                }
            }
        }
        public WeaponActions @Weapon => new WeaponActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IMovementActions
        {
            void OnDirection(InputAction.CallbackContext context);
        }
        public interface IRotationActions
        {
            void OnPosition(InputAction.CallbackContext context);
        }
        public interface IWeaponActions
        {
            void OnShoot(InputAction.CallbackContext context);
        }
    }
}
