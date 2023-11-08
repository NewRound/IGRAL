using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageObject : InteractiveObject
{
    private bool isUsed = false;

    public override void Use()
    {
        if (CheckCondition() && !isUsed)
        {
            isUsed = true;
            MapGenerator.Instance.InstantiateStage();
        }
    }

    public virtual bool CheckCondition()
    {
        return true;
    }
}
