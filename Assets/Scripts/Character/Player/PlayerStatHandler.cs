using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : CharacterStatHandler
{
    public PlayerData Data { get; private set; }

    public PlayerStatHandler(PlayerData data)
    {
        Data = data;
    }
}
