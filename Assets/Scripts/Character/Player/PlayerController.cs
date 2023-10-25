using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : InputController
{
    [SerializeField] private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }

    private void Awake()
    {
        StatHandler = new PlayerStatHandler(stat);
    }

}
