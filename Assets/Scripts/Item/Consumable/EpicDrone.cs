public class EpicDrone : ItemConsumable
{

    public override void UseConsumable()
    {
        base.UseConsumable();
        GameManager.Instance.drone.ActiveDrone(200f);
    }

}
