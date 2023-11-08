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
        //인벤토리로 이동
        switch (item.ItemType)
        {
            case ItemType.Artifact:
                UIInventory.Instance.AddItem(ItemManager.Instance.itemSO);
                UIController.Instance.SwitchingAttack();
                ItemManager.Instance.DelSetPickupItem();
                break;
            case ItemType.Consumable:
                //TODO 소비형 아이템 교체 함수 구현 필요
                UIController.Instance.SwitchingAttack();
                ItemManager.Instance.DelSetPickupItem();
                break;
            case ItemType.Ingredient:
                //TODO 재료 아이템은 충돌시 바로 추가되는 함수 구현 필요
                break;
            case ItemType.Weapon:
                //TODO 무기 교체 함수 구현 필요

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
