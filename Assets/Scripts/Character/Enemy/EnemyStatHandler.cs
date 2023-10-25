using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : CharacterStatHandler
{
    public EnemySO Data { get; private set; }

    public EnemyStatHandler(EnemySO data)
    {
        Data = data;
    }
}
