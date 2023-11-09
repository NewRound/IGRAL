using UnityEngine;

public class UniqueDrone : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();
        Debug.Log("UniqueDrone");
    }
}
