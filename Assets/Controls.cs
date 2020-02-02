// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e21a8615-6bec-41a3-8baa-40bf5749db2b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f0e7971f-172f-4fb5-8cdd-086addddc485"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""ed270ca9-3821-4fc5-a60e-8236b058aef6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""5ffda956-f80c-48bf-bd96-89a7e5164b37"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d5dd71f0-9245-4cc4-9511-186a4441da96"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""33333424-df77-49fe-9923-216935ea611a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""40f9ea59-9512-464a-99e2-e320f4734ae1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""dbb8019d-1828-46b2-841b-4e398f9137d0"",
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
                    ""id"": ""8342a9fb-3a89-4812-81c9-ae5b0654066b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7289e657-5171-404a-bdc5-84dfa0fe8119"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3ce54c23-2e7f-43e7-a900-82f712912732"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cdce56fd-cb94-42ea-898b-a328a527419c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c744f027-3ddf-47fe-b1f7-0abd0a14a163"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""590577b6-2312-4537-bfd2-4e1291487a78"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad29231f-8cc7-4cd5-b45c-c80e5933a4c8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfede804-8466-4750-9f0a-d5ed1d4bd67b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f1affef-d2ae-4a2c-bd69-8aa847814daa"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4fef045-e7e1-4700-b761-9c847ca1b696"",
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
                    ""id"": ""e2a13cc1-7f07-4edf-bcbc-705b8b30bbd3"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f5a2372-03c2-4d1d-94e1-54c108392ba6"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""899f0ac7-b891-4505-935e-4f5503a729b6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""caf37058-45b8-4cad-b764-3147e2deb7f1"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""b3fdf291-2c0d-4919-853f-458ef949aa94"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""15ce1b30-a641-43e5-abe3-ec2f52f90afe"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Hero"",
            ""id"": ""6306a990-89a9-41ae-a559-14280597b781"",
            ""actions"": [
                {
                    ""name"": ""South"",
                    ""type"": ""Button"",
                    ""id"": ""d879d867-6095-4a24-96b3-d7554c096bda"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""North"",
                    ""type"": ""Button"",
                    ""id"": ""d1aa6ab8-e8ab-4149-a9e7-33ced4136b69"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""West"",
                    ""type"": ""Button"",
                    ""id"": ""d63e8761-e82d-4ec5-9777-25b8dc43b7d9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""East"",
                    ""type"": ""Button"",
                    ""id"": ""7e372302-3f7e-4709-88bb-0df5123ffd47"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""de9c25e6-689b-4665-85f1-3f2c166c950a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""South"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6e44f4a-dec5-47f4-b55b-36ee85ff0930"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""North"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""211697c5-2f4a-4dda-99d4-56e1402c6ad4"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""West"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""640de74b-d03c-4d4d-8d09-054c8fcb3c59"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""East"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""QTEMinigame"",
            ""id"": ""f1b87e40-30d6-4f44-a56c-468bb713bbbc"",
            ""actions"": [
                {
                    ""name"": ""Button1"",
                    ""type"": ""Button"",
                    ""id"": ""c5553f9f-c800-4748-b6f8-b8dee747c551"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button2"",
                    ""type"": ""Button"",
                    ""id"": ""7164036b-a90b-4856-91cf-4d7c2a98b22d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button3"",
                    ""type"": ""Button"",
                    ""id"": ""caef962b-fb8b-4b96-a6c9-09ceadca20a6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button4"",
                    ""type"": ""Button"",
                    ""id"": ""14ff3c24-8810-4ad9-9847-c80d745e1844"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90164a94-f110-405e-8ba4-97094dc09f7f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(pressPoint=0.5)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cbdacf2-25b0-4824-8b52-b6f3d713fd91"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0913f5fb-1896-4bce-91c9-219ca291bd7b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fad94b2-3faf-4627-846b-012b15a07126"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6219ef6b-a321-48f0-8aaf-c9768780be7b"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77889afb-e512-4052-b302-80540f0ace15"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7787b4f0-6334-4278-bca6-4c04ca087bbc"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3d1769f-66bf-4de0-8c2a-a255cdfe1fb4"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GuitarHeroMinigame"",
            ""id"": ""43de0680-a344-4423-9c6f-9052c7e3c446"",
            ""actions"": [
                {
                    ""name"": ""Trigger"",
                    ""type"": ""Button"",
                    ""id"": ""8d1e067b-7a7c-4abe-93a5-0261d0cb22c1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d2cc7ada-46ee-4188-ac03-3e497e5f8552"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00707651-639d-4ae2-aa0a-4cdfbdd437f8"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SpamMinigame"",
            ""id"": ""d1b288d0-c663-4ca5-ab24-4e101e3174b1"",
            ""actions"": [
                {
                    ""name"": ""SPAM"",
                    ""type"": ""Button"",
                    ""id"": ""b2650031-496a-4d69-8e4f-6478baaff224"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3b836d3a-6f94-448c-97d4-74cb922a10e4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SPAM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cd16b60-052d-4813-9585-d4593eef1920"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SPAM"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SpinMinigame"",
            ""id"": ""e669c4ee-a568-40e2-9913-9d43ace65b3b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""8085556e-d544-4fdf-a2a4-df32237cad01"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e888bf5b-f87b-4298-a864-3d9ef20bcde8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""fb68d762-1f3f-4326-8b9a-621e079b34f6"",
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
                    ""id"": ""d436fb55-fca2-444a-8c92-8a08aae29d63"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aed67b6c-8806-4849-90a8-a32370bd5417"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5088756e-ce01-4b5e-9ec5-2d852101d225"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b809ea83-f5d6-4b7d-ba8d-bbfb0fdb8a56"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Start = m_Player.FindAction("Start", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
        // Hero
        m_Hero = asset.FindActionMap("Hero", throwIfNotFound: true);
        m_Hero_South = m_Hero.FindAction("South", throwIfNotFound: true);
        m_Hero_North = m_Hero.FindAction("North", throwIfNotFound: true);
        m_Hero_West = m_Hero.FindAction("West", throwIfNotFound: true);
        m_Hero_East = m_Hero.FindAction("East", throwIfNotFound: true);
        // QTEMinigame
        m_QTEMinigame = asset.FindActionMap("QTEMinigame", throwIfNotFound: true);
        m_QTEMinigame_Button1 = m_QTEMinigame.FindAction("Button1", throwIfNotFound: true);
        m_QTEMinigame_Button2 = m_QTEMinigame.FindAction("Button2", throwIfNotFound: true);
        m_QTEMinigame_Button3 = m_QTEMinigame.FindAction("Button3", throwIfNotFound: true);
        m_QTEMinigame_Button4 = m_QTEMinigame.FindAction("Button4", throwIfNotFound: true);
        // GuitarHeroMinigame
        m_GuitarHeroMinigame = asset.FindActionMap("GuitarHeroMinigame", throwIfNotFound: true);
        m_GuitarHeroMinigame_Trigger = m_GuitarHeroMinigame.FindAction("Trigger", throwIfNotFound: true);
        // SpamMinigame
        m_SpamMinigame = asset.FindActionMap("SpamMinigame", throwIfNotFound: true);
        m_SpamMinigame_SPAM = m_SpamMinigame.FindAction("SPAM", throwIfNotFound: true);
        // SpinMinigame
        m_SpinMinigame = asset.FindActionMap("SpinMinigame", throwIfNotFound: true);
        m_SpinMinigame_Movement = m_SpinMinigame.FindAction("Movement", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Start;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Start => m_Wrapper.m_Player_Start;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Start.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStart;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Newaction;
    public struct UIActions
    {
        private @Controls m_Wrapper;
        public UIActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_UI_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Hero
    private readonly InputActionMap m_Hero;
    private IHeroActions m_HeroActionsCallbackInterface;
    private readonly InputAction m_Hero_South;
    private readonly InputAction m_Hero_North;
    private readonly InputAction m_Hero_West;
    private readonly InputAction m_Hero_East;
    public struct HeroActions
    {
        private @Controls m_Wrapper;
        public HeroActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @South => m_Wrapper.m_Hero_South;
        public InputAction @North => m_Wrapper.m_Hero_North;
        public InputAction @West => m_Wrapper.m_Hero_West;
        public InputAction @East => m_Wrapper.m_Hero_East;
        public InputActionMap Get() { return m_Wrapper.m_Hero; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HeroActions set) { return set.Get(); }
        public void SetCallbacks(IHeroActions instance)
        {
            if (m_Wrapper.m_HeroActionsCallbackInterface != null)
            {
                @South.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnSouth;
                @South.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnSouth;
                @South.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnSouth;
                @North.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnNorth;
                @North.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnNorth;
                @North.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnNorth;
                @West.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnWest;
                @West.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnWest;
                @West.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnWest;
                @East.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnEast;
                @East.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnEast;
                @East.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnEast;
            }
            m_Wrapper.m_HeroActionsCallbackInterface = instance;
            if (instance != null)
            {
                @South.started += instance.OnSouth;
                @South.performed += instance.OnSouth;
                @South.canceled += instance.OnSouth;
                @North.started += instance.OnNorth;
                @North.performed += instance.OnNorth;
                @North.canceled += instance.OnNorth;
                @West.started += instance.OnWest;
                @West.performed += instance.OnWest;
                @West.canceled += instance.OnWest;
                @East.started += instance.OnEast;
                @East.performed += instance.OnEast;
                @East.canceled += instance.OnEast;
            }
        }
    }
    public HeroActions @Hero => new HeroActions(this);

    // QTEMinigame
    private readonly InputActionMap m_QTEMinigame;
    private IQTEMinigameActions m_QTEMinigameActionsCallbackInterface;
    private readonly InputAction m_QTEMinigame_Button1;
    private readonly InputAction m_QTEMinigame_Button2;
    private readonly InputAction m_QTEMinigame_Button3;
    private readonly InputAction m_QTEMinigame_Button4;
    public struct QTEMinigameActions
    {
        private @Controls m_Wrapper;
        public QTEMinigameActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Button1 => m_Wrapper.m_QTEMinigame_Button1;
        public InputAction @Button2 => m_Wrapper.m_QTEMinigame_Button2;
        public InputAction @Button3 => m_Wrapper.m_QTEMinigame_Button3;
        public InputAction @Button4 => m_Wrapper.m_QTEMinigame_Button4;
        public InputActionMap Get() { return m_Wrapper.m_QTEMinigame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QTEMinigameActions set) { return set.Get(); }
        public void SetCallbacks(IQTEMinigameActions instance)
        {
            if (m_Wrapper.m_QTEMinigameActionsCallbackInterface != null)
            {
                @Button1.started -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton1;
                @Button1.performed -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton1;
                @Button1.canceled -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton1;
                @Button2.started -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton2;
                @Button2.performed -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton2;
                @Button2.canceled -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton2;
                @Button3.started -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton3;
                @Button3.performed -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton3;
                @Button3.canceled -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton3;
                @Button4.started -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton4;
                @Button4.performed -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton4;
                @Button4.canceled -= m_Wrapper.m_QTEMinigameActionsCallbackInterface.OnButton4;
            }
            m_Wrapper.m_QTEMinigameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Button1.started += instance.OnButton1;
                @Button1.performed += instance.OnButton1;
                @Button1.canceled += instance.OnButton1;
                @Button2.started += instance.OnButton2;
                @Button2.performed += instance.OnButton2;
                @Button2.canceled += instance.OnButton2;
                @Button3.started += instance.OnButton3;
                @Button3.performed += instance.OnButton3;
                @Button3.canceled += instance.OnButton3;
                @Button4.started += instance.OnButton4;
                @Button4.performed += instance.OnButton4;
                @Button4.canceled += instance.OnButton4;
            }
        }
    }
    public QTEMinigameActions @QTEMinigame => new QTEMinigameActions(this);

    // GuitarHeroMinigame
    private readonly InputActionMap m_GuitarHeroMinigame;
    private IGuitarHeroMinigameActions m_GuitarHeroMinigameActionsCallbackInterface;
    private readonly InputAction m_GuitarHeroMinigame_Trigger;
    public struct GuitarHeroMinigameActions
    {
        private @Controls m_Wrapper;
        public GuitarHeroMinigameActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Trigger => m_Wrapper.m_GuitarHeroMinigame_Trigger;
        public InputActionMap Get() { return m_Wrapper.m_GuitarHeroMinigame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GuitarHeroMinigameActions set) { return set.Get(); }
        public void SetCallbacks(IGuitarHeroMinigameActions instance)
        {
            if (m_Wrapper.m_GuitarHeroMinigameActionsCallbackInterface != null)
            {
                @Trigger.started -= m_Wrapper.m_GuitarHeroMinigameActionsCallbackInterface.OnTrigger;
                @Trigger.performed -= m_Wrapper.m_GuitarHeroMinigameActionsCallbackInterface.OnTrigger;
                @Trigger.canceled -= m_Wrapper.m_GuitarHeroMinigameActionsCallbackInterface.OnTrigger;
            }
            m_Wrapper.m_GuitarHeroMinigameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Trigger.started += instance.OnTrigger;
                @Trigger.performed += instance.OnTrigger;
                @Trigger.canceled += instance.OnTrigger;
            }
        }
    }
    public GuitarHeroMinigameActions @GuitarHeroMinigame => new GuitarHeroMinigameActions(this);

    // SpamMinigame
    private readonly InputActionMap m_SpamMinigame;
    private ISpamMinigameActions m_SpamMinigameActionsCallbackInterface;
    private readonly InputAction m_SpamMinigame_SPAM;
    public struct SpamMinigameActions
    {
        private @Controls m_Wrapper;
        public SpamMinigameActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SPAM => m_Wrapper.m_SpamMinigame_SPAM;
        public InputActionMap Get() { return m_Wrapper.m_SpamMinigame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpamMinigameActions set) { return set.Get(); }
        public void SetCallbacks(ISpamMinigameActions instance)
        {
            if (m_Wrapper.m_SpamMinigameActionsCallbackInterface != null)
            {
                @SPAM.started -= m_Wrapper.m_SpamMinigameActionsCallbackInterface.OnSPAM;
                @SPAM.performed -= m_Wrapper.m_SpamMinigameActionsCallbackInterface.OnSPAM;
                @SPAM.canceled -= m_Wrapper.m_SpamMinigameActionsCallbackInterface.OnSPAM;
            }
            m_Wrapper.m_SpamMinigameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SPAM.started += instance.OnSPAM;
                @SPAM.performed += instance.OnSPAM;
                @SPAM.canceled += instance.OnSPAM;
            }
        }
    }
    public SpamMinigameActions @SpamMinigame => new SpamMinigameActions(this);

    // SpinMinigame
    private readonly InputActionMap m_SpinMinigame;
    private ISpinMinigameActions m_SpinMinigameActionsCallbackInterface;
    private readonly InputAction m_SpinMinigame_Movement;
    public struct SpinMinigameActions
    {
        private @Controls m_Wrapper;
        public SpinMinigameActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_SpinMinigame_Movement;
        public InputActionMap Get() { return m_Wrapper.m_SpinMinigame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpinMinigameActions set) { return set.Get(); }
        public void SetCallbacks(ISpinMinigameActions instance)
        {
            if (m_Wrapper.m_SpinMinigameActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_SpinMinigameActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_SpinMinigameActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_SpinMinigameActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_SpinMinigameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public SpinMinigameActions @SpinMinigame => new SpinMinigameActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IHeroActions
    {
        void OnSouth(InputAction.CallbackContext context);
        void OnNorth(InputAction.CallbackContext context);
        void OnWest(InputAction.CallbackContext context);
        void OnEast(InputAction.CallbackContext context);
    }
    public interface IQTEMinigameActions
    {
        void OnButton1(InputAction.CallbackContext context);
        void OnButton2(InputAction.CallbackContext context);
        void OnButton3(InputAction.CallbackContext context);
        void OnButton4(InputAction.CallbackContext context);
    }
    public interface IGuitarHeroMinigameActions
    {
        void OnTrigger(InputAction.CallbackContext context);
    }
    public interface ISpamMinigameActions
    {
        void OnSPAM(InputAction.CallbackContext context);
    }
    public interface ISpinMinigameActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
