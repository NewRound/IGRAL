using UnityEngine;

public class DroneItemDrop : MonoBehaviour
{

    [SerializeField] private ItemConsumable drone;
    private PickupItem itemBox;

    private void Start()
    {
        itemBox = Resources.Load<PickupItem>("Items/ItemBox");

        PickupItem pickupItem = Instantiate(itemBox);
        pickupItem.DropConsumableItemSet(drone);
        pickupItem.transform.position = transform.position;
    }

}
