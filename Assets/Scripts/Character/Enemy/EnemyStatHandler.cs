using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : CharacterStatHandler
{
    public EnemyData Data { get; private set; }

    public EnemyStatHandler(EnemyData data)
    {
        Data = data;
    }
}
