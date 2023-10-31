using UnityEngine;

public enum ItemType { Weapon = 1, Artifact, Consumable, Ingredient }
public enum Rarity { Normal = 1, Rare, Unique, Epic }

public class Item : PickupObject
{
    [field: Header("# Base Info")]    
    [field: SerializeField] public string ItemName { get; private set;}
    [field: SerializeField] public int ItemID { get; private set; }
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [field: SerializeField] public Rarity ItemRarity { get; private set; }
    [field: SerializeField] public float DropProbability { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set;}
    [field: SerializeField] public GameObject ItemObject { get; private set; }
    [field :TextArea][field: SerializeField] public string ItemInfo { get; private set;}
    [field: SerializeField] public int Price { get; private set;}
    [field: SerializeField] public bool IsStackable { get; private set;}
    [field: SerializeField] public int MaxStack { get; private set;}
    [field: SerializeField] public PlayerSO itemData { get; private set; }

    public override void Pickup()
    {
        base.Pickup();
        gameObject.SetActive(false);
    }

    public void OnUseItem()
    {
        if(ItemType == ItemType.Consumable)
        {
            
        }
    }


}
