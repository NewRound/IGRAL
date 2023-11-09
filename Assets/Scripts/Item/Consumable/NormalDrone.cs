using UnityEngine;


public class NormalDrone : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();
        Debug.Log("NormalDrone");
    }
}
