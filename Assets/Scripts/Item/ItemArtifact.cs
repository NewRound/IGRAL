using UnityEngine;

public class ItemArtifact : PickupArtifact, IItem
{
    [field: Header("# Artifact Info")]
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public int ItemID { get; private set; }
    [field: SerializeField] public Rarity ItemRarity { get; private set; }
    [field: SerializeField] public float DropProbability { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set; }
    [field: SerializeField] public GameObject ItemObject { get; private set; }
    [field: TextArea][field: SerializeField] public string ItemInfo { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public PlayerSO ItemData { get; private set; }
    [field: SerializeField] public StatChange[] ItemDatas { get; private set; }

    public override void Pickup()
    {
        base.Pickup();
        gameObject.SetActive(false);
    }
}
