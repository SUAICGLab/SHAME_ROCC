﻿using UnityEngine;

public class FlyPlayer : BasePlayer
{
    public override void Start()
    {
        base.Start();

        if (isLocalPlayer)
        {
            if (transform.Find("Camera").GetComponentInChildren<Camera>())
                transform.Find("Camera").tag = "MainCamera";
            if (transform.Find("Camera/Canvas"))
                SetLayerRecursively(transform.Find("Camera/Canvas").gameObject, Layer.OwnedUI);
        }
    }
}
