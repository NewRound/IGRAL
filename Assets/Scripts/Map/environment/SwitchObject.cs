using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : InteractiveObject
{
    [SerializeField] List<GameObject> ConnectedObject;

    IObject connectedObject;

    public override void Use()
    {
        Debug.Log("½ÇÇà");
        if (CheckCondition())
        {
            foreach (GameObject obj in ConnectedObject)
            {
                connectedObject = obj.GetComponent<IObject>();
                connectedObject.Use();
            }
        }
    }

    public virtual bool CheckCondition()
    {
        return true;
    }
}
