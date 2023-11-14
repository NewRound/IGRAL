using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : InteractiveObject
{
    [SerializeField] List<GameObject> ConnectedObject;
    [SerializeField] Area targetArea;

    IObject connectedObject;

    public override void Use()
    {
        Debug.Log("½ÇÇà");
        if (CheckCondition() && checkEnemiesInArea())
        {
            foreach (GameObject obj in ConnectedObject)
            {
                connectedObject = obj.GetComponent<IObject>();
                connectedObject.Use();
            }
        }
    }

    private bool checkEnemiesInArea()
    {
        if (targetArea == null)
            return true;
        else
        {
            if(targetArea.enemys.Count == 0)
                return true;
            else
                return false;
        }
    }

    public virtual bool CheckCondition()
    {
        return true;
    }
}
