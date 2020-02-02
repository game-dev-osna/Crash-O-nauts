using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class SkinSelecter : MonoBehaviour
{
    //Sound
    private AudioSource source;
    private AudioClip newSkinSound;

    [HideInInspector]
    public InputUser user;

    public Controls controls;

    private Vector3 moveInput;

    private RandomSkin m_RandomSkin;

    private int id;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Interact.started += delegate { OnInteractButtonPressed(); };
        controls.Player.Start.started += delegate { OnStartPressed(); };

        m_RandomSkin = GetComponentInChildren<RandomSkin>();

        source = GameObject.Find("GameManager").GetComponent<AudioSource>();
        newSkinSound = Resources.Load<AudioClip>("SFX/UI/UI");
    }

    private void OnInteractButtonPressed()
    {
        source.PlayOneShot(newSkinSound);
        m_RandomSkin.ToggleSkin(PlayerSkinManager.Instance.ToggleSelection(id));
    }

    private void OnStartPressed()
    {
        MenuController.Instance.StartGameSound();
        GameManager.Instance.NewGame();
    }

    public void Init(InputUser user, int id)
    {
        this.id = id;
        this.user = user;
        user.AssociateActionsWithUser(controls);

        GetComponentInChildren<RandomColor>().SetColor(PlayerSkinManager.Instance.Colors[id]);

        m_RandomSkin.ToggleSkin(PlayerSkinManager.Instance.GetSelection(id));
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