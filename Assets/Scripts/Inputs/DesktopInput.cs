﻿using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// В зависимости от ввода игрока этот класс вызывает функции управления игровым персонажем 
/// </summary>
[RequireComponent(typeof(PlayerDesktop), typeof(MoverDesktop))]
public class DesktopInput : NetworkBehaviour
{
    PlayerDesktop player;
    MoverDesktop mover;

    void Start()
    {
        player = GetComponent<PlayerDesktop>();
        mover = GetComponent<MoverDesktop>();
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetAxis("Mouse X") != 0)
            mover.Rotate(Input.GetAxis("Mouse X"));

        if (Input.GetAxis("Mouse Y") != 0)
            mover.TiltHead(Input.GetAxis("Mouse Y"));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            mover.Strafe(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetButton("Jump"))
            mover.Jump();

        if (Input.GetButton("Fire1"))
            player.FireGun();

        if (Input.GetButtonDown("Fire3"))
            player.SwitchGun();
    }
}