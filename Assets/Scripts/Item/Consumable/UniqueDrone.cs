using UnityEngine;

public class UniqueDrone : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();
        GameManager.Instance.drone.ActiveDrone(100f);
    }
}
