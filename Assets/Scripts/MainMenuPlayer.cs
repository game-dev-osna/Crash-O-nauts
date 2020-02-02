using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class MainMenuPlayer : MonoBehaviour
{
    [HideInInspector]
    public InputUser user;

    public Controls controls;


    private void Awake()
    {
        controls = new Controls();

        controls.Player.Start.started += delegate { OnStartPressed(); };
    }

    private void OnStartPressed()
    {
        GameManager.Instance.LoadCharacterSelection();
    }

    public void Init(InputUser user)
    {
        this.user = user;
        user.AssociateActionsWithUser(controls);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}