//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/InputActions/PlayerInputAction.inputactions
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

public partial class @PlayerInputAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3d917e4b-8450-4272-b182-64de435c9ab6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5c22e13f-2cb4-4b1c-b504-053991645e7c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8d6bf93e-78c9-4e0e-8b21-c5156154a15f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""fa994598-2506-4cdb-a984-6a9a91aa9277"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""c8e2c53f-c1bc-4701-8f00-1dbaf2e4a7a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkinMutant"",
                    ""type"": ""Button"",
                    ""id"": ""a62cb69c-1752-453b-ba7c-606f50e39a97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BladeMutant"",
                    ""type"": ""Button"",
                    ""id"": ""99fb88f5-0531-4743-90b8-c256e0e9903c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HammerMutant"",
                    ""type"": ""Button"",
                    ""id"": ""bef96b41-4a2f-493e-8d7c-e0f9b0b6d60e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PsychometricMutant"",
                    ""type"": ""Button"",
                    ""id"": ""63c09812-9986-4351-8ba5-e64ab38ff255"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""b0a7cc40-013b-42da-b376-06e831702d84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""062355ad-84ab-48a3-9e93-3e76b48b2b26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""659c6cb6-f50f-4974-af33-5526c70e5aa6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7a732556-a8cf-4333-a996-818fa66b2cc9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""112c358a-d178-4b93-9dec-faef4d6c88bc"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e15d99ab-b292-40f9-a002-4fe0d15fae48"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""09ebe840-75c9-479a-b4eb-dd86d6139d8a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0bd29a65-746b-46ad-86e4-f051df6b359b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ac9f5b1-efa6-49a0-bb2f-c240e55cb94a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b80536f-ae42-4878-8524-2ea81a8fb1d4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""445e025e-3f71-4e28-8691-488b81bce2d1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91a6ae18-91a2-463d-b010-983d06bba7f2"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""224e09d9-08db-48ef-a491-1d1bcf5f6674"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkinMutant"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""946982db-5ad9-42ee-8792-098cf06d1701"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BladeMutant"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fec2de8-2788-48a3-a72c-a1fda0efa09e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HammerMutant"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b38d3f62-167e-4e02-a1a5-a8db52e7b64e"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PsychometricMutant"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerControl"",
            ""bindingGroup"": ""PlayerControl"",
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
                },
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Roll = m_Player.FindAction("Roll", throwIfNotFound: true);
        m_Player_SkinMutant = m_Player.FindAction("SkinMutant", throwIfNotFound: true);
        m_Player_BladeMutant = m_Player.FindAction("BladeMutant", throwIfNotFound: true);
        m_Player_HammerMutant = m_Player.FindAction("HammerMutant", throwIfNotFound: true);
        m_Player_PsychometricMutant = m_Player.FindAction("PsychometricMutant", throwIfNotFound: true);
        m_Player_UseItem = m_Player.FindAction("UseItem", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Roll;
    private readonly InputAction m_Player_SkinMutant;
    private readonly InputAction m_Player_BladeMutant;
    private readonly InputAction m_Player_HammerMutant;
    private readonly InputAction m_Player_PsychometricMutant;
    private readonly InputAction m_Player_UseItem;
    private readonly InputAction m_Player_Interact;
    public struct PlayerActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Roll => m_Wrapper.m_Player_Roll;
        public InputAction @SkinMutant => m_Wrapper.m_Player_SkinMutant;
        public InputAction @BladeMutant => m_Wrapper.m_Player_BladeMutant;
        public InputAction @HammerMutant => m_Wrapper.m_Player_HammerMutant;
        public InputAction @PsychometricMutant => m_Wrapper.m_Player_PsychometricMutant;
        public InputAction @UseItem => m_Wrapper.m_Player_UseItem;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Roll.started += instance.OnRoll;
            @Roll.performed += instance.OnRoll;
            @Roll.canceled += instance.OnRoll;
            @SkinMutant.started += instance.OnSkinMutant;
            @SkinMutant.performed += instance.OnSkinMutant;
            @SkinMutant.canceled += instance.OnSkinMutant;
            @BladeMutant.started += instance.OnBladeMutant;
            @BladeMutant.performed += instance.OnBladeMutant;
            @BladeMutant.canceled += instance.OnBladeMutant;
            @HammerMutant.started += instance.OnHammerMutant;
            @HammerMutant.performed += instance.OnHammerMutant;
            @HammerMutant.canceled += instance.OnHammerMutant;
            @PsychometricMutant.started += instance.OnPsychometricMutant;
            @PsychometricMutant.performed += instance.OnPsychometricMutant;
            @PsychometricMutant.canceled += instance.OnPsychometricMutant;
            @UseItem.started += instance.OnUseItem;
            @UseItem.performed += instance.OnUseItem;
            @UseItem.canceled += instance.OnUseItem;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Roll.started -= instance.OnRoll;
            @Roll.performed -= instance.OnRoll;
            @Roll.canceled -= instance.OnRoll;
            @SkinMutant.started -= instance.OnSkinMutant;
            @SkinMutant.performed -= instance.OnSkinMutant;
            @SkinMutant.canceled -= instance.OnSkinMutant;
            @BladeMutant.started -= instance.OnBladeMutant;
            @BladeMutant.performed -= instance.OnBladeMutant;
            @BladeMutant.canceled -= instance.OnBladeMutant;
            @HammerMutant.started -= instance.OnHammerMutant;
            @HammerMutant.performed -= instance.OnHammerMutant;
            @HammerMutant.canceled -= instance.OnHammerMutant;
            @PsychometricMutant.started -= instance.OnPsychometricMutant;
            @PsychometricMutant.performed -= instance.OnPsychometricMutant;
            @PsychometricMutant.canceled -= instance.OnPsychometricMutant;
            @UseItem.started -= instance.OnUseItem;
            @UseItem.performed -= instance.OnUseItem;
            @UseItem.canceled -= instance.OnUseItem;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_PlayerControlSchemeIndex = -1;
    public InputControlScheme PlayerControlScheme
    {
        get
        {
            if (m_PlayerControlSchemeIndex == -1) m_PlayerControlSchemeIndex = asset.FindControlSchemeIndex("PlayerControl");
            return asset.controlSchemes[m_PlayerControlSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnSkinMutant(InputAction.CallbackContext context);
        void OnBladeMutant(InputAction.CallbackContext context);
        void OnHammerMutant(InputAction.CallbackContext context);
        void OnPsychometricMutant(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
