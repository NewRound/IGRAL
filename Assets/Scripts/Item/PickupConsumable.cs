using UnityEngine;

public class PickupConsumable : MonoBehaviour
{
    [SerializeField] private LayerMask canBePickupBy;

    public virtual void Pickup()
    {
        //TODO 소비형 아이템 교체 함수 구현 필요

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
