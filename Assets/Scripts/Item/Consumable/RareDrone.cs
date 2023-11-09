using UnityEngine;

public class RareDrone : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();
        Debug.Log("RareDrone");
    }
}
