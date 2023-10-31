using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageObject : InteractiveObject
{
    public override void Use()
    {
        if (CheckCondition())
        {
            MapGenerator.Instance.InstantiateStage();
        }
    }

    public virtual bool CheckCondition()
    {
        return true;
    }
}
