using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : CharacterStatHandler
{
    public PlayerSO Data { get; private set; }

    public PlayerStatHandler(PlayerSO data)
    {
        Data = data;
    }
}
