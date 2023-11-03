using UnityEngine;

public class PickupArtifact : MonoBehaviour
{
    [SerializeField] private LayerMask canBePickupBy;

    public virtual void Pickup()
    {
        //인벤토리로 이동
        UIInventory.Instance.AddItem(ItemManager.Instance.pickupItem.GetComponent<IItem>());
        UIController.Instance.SwitchingAttack();
        ItemManager.Instance.DelSetPickupItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            ItemManager.Instance.SetPickupItem(this.gameObject);
            UIController.Instance.SwitchingPickup();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            ItemManager.Instance.DelSetPickupItem();
            UIController.Instance.SwitchingAttack();
        }
    }
}
