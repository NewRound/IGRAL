using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "SO/Item/ItemData")]
public class ItemSO : ScriptableObject
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
    [field: SerializeField] public LayerMask canBePickupBy { get; private set; }
    [field: SerializeField] public StatChange[] ItemDatas { get; private set; }

}
