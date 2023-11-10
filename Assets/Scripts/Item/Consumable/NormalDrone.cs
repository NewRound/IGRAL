using UnityEngine;


public class NormalDrone : ItemConsumable
{

    public override void UseConsumable()
    {
        base.UseConsumable();
        GameManager.Instance.drone.ActiveDrone(30f);
    }
}
