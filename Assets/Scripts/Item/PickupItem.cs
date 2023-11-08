using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemSO item;

    public void PickupItemSet(ItemSO dropItem)
    {
        Debug.Log(dropItem.ItemType);
        item = dropItem;
    }

    public void Pickup()
    {
        Debug.Log(item.ItemType);
        //�κ��丮�� �̵�
        switch (item.ItemType)
        {
            case ItemType.Artifact:
                UIInventory.Instance.AddItem(ItemManager.Instance.itemSO);
                UIController.Instance.SwitchingAttack();
                ItemManager.Instance.DelSetPickupItem();
                break;
            case ItemType.Consumable:
                //TODO �Һ��� ������ ��ü �Լ� ���� �ʿ�
                UIController.Instance.SwitchingAttack();
                ItemManager.Instance.DelSetPickupItem();
                break;
            case ItemType.Ingredient:
                //TODO ��� �������� �浹�� �ٷ� �߰��Ǵ� �Լ� ���� �ʿ�
                break;
            case ItemType.Weapon:
                //TODO ���� ��ü �Լ� ���� �ʿ�

                UIController.Instance.SwitchingAttack();
                ItemManager.Instance.DelSetPickupItem();
                break;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (item.ItemType)
        {
            case ItemType.Artifact:
            case ItemType.Consumable:
            case ItemType.Weapon:
                if (item.canBePickupBy.value == (item.canBePickupBy.value | (1 << other.gameObject.layer)))
                {
                    ItemManager.Instance.SetPickupItem(item, this);
                    UIController.Instance.SwitchingPickup();
                    UIManager.Instance.OpenUI<UIItemPopup>().OpenItemPopup(item);
                }
                break;
            case ItemType.Ingredient:
                Pickup();
                break;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        switch (item.ItemType)
        {
            case ItemType.Artifact:
            case ItemType.Consumable:
            case ItemType.Weapon:
                if (item.canBePickupBy.value == (item.canBePickupBy.value | (1 << other.gameObject.layer)))
                {
                    ItemManager.Instance.DelSetPickupItem();
                    UIController.Instance.SwitchingAttack();
                    UIManager.Instance.CloseUI<UIItemPopup>().CloseItemPopup();
                }
                break;
        }
    }

}
