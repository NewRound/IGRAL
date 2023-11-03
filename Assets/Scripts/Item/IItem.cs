using UnityEngine;

public interface IItem 
{
    public ItemType ItemType { get; }

    public string ItemName { get; }
    public int ItemID { get; }
    public Rarity ItemRarity { get; }
    public float DropProbability { get; }
    public Sprite ItemIcon { get; }
    public GameObject ItemObject { get; }
    public string ItemInfo { get; }
    public int Price { get; }
    public PlayerSO ItemData { get; }
 
    public void Pickup();
}
