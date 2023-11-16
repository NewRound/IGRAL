using UnityEngine;

public class EpicDrone : ItemConsumable
{

    public override void UseConsumable()
    {
        base.UseConsumable();

        GameObject go = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.Drone);
        Drone drone = go.GetComponent<Drone>();
        drone.ActiveDrone(200f);
    }

}
