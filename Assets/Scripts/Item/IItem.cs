using UnityEngine;

public class Item : MonoBehaviour
{
    [field: Header("ItemInfo")]
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public int ItemID { get; private set; }
    [field: SerializeField] public Rarity ItemRarity { get; private set; }
    [field: SerializeField] public int DropProbability { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set; }
    [field: TextArea][field: SerializeField] public string ItemInfo { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public StatChange[] ItemDatas { get; private set; }

    [SerializeField] protected LayerMask canBePickupBy;

    public virtual void Pickup()
    {
        gameObject.SetActive(false);
    }

    public void DropSet(Item item)
    {
        ItemType = item.ItemType;
        ItemName = item.ItemName;
        ItemID = item.ItemID;
        ItemRarity = item.ItemRarity;
        DropProbability = item.DropProbability;
        ItemIcon = item.ItemIcon;
        ItemInfo = item.ItemInfo;
        Price = item.Price;
        ItemDatas = item.ItemDatas;
        canBePickupBy = item.canBePickupBy;
    }
}
