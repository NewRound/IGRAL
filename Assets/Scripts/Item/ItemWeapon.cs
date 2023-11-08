using UnityEngine;

public class ItemWeapon : Item
{
    [field: SerializeField] public GameObject ItemObject { get; private set; }

    public override void Pickup()
    {
        base.Pickup();
        //TODO ���� ��ü �Լ� ���� �ʿ�

        UIController.Instance.SwitchingAttack();
        ItemManager.Instance.DelSetPickupItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            ItemManager.Instance.SetPickupItem(this.gameObject);
            UIController.Instance.SwitchingPickup();
            UIManager.Instance.OpenUI<UIItemPopup>().OpenItemPopup(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            ItemManager.Instance.DelSetPickupItem();
            UIController.Instance.SwitchingAttack();
            UIManager.Instance.CloseUI<UIItemPopup>().CloseItemPopup();
        }
    }
}
