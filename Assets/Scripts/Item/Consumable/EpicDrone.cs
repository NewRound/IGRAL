using UnityEngine;

public class EpicDrone : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();

        Debug.Log("EpicDrone");
    }
}
