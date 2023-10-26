using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Weapon, Artifact, Consumable, ingredient, ETC}
    public enum Rarity { Normal, Rare, Unique, Epic}

    [field: Header("# Base Info")]
    [field: SerializeField] public string ItemName { get; private set;}
    [field: SerializeField] public Type ItemType { get; private set; }
    [field: SerializeField] public Rarity ItemRarity { get; private set; }
    [field: SerializeField] public float DropProbability { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set;}
    [field: SerializeField] public GameObject ItemObject { get; private set; }
    [field :TextArea][field: SerializeField] public string ItemInfo { get; private set;}
    [field: SerializeField] public int buyPrice { get; private set;}
    [field: SerializeField] public int sellPrice { get; private set;}
    [field: SerializeField] public bool isStackable { get; private set;}
    [field: SerializeField] public int maxStack { get; private set;}
    [field: SerializeField] public PlayerSO itemData { get; private set; }       
}
