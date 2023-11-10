using UnityEngine;

public class RareDrone : ItemConsumable
{
    public override void UseConsumable()
    {
        base.UseConsumable();
        GameManager.Instance.drone.ActiveDrone(60f);
    }
}
