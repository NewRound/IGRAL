using UnityEngine;

public class Item : MonoBehaviour
{
    [field: Header("# Base Info")]
    [field: SerializeField] public string ItemName { get; private set;}
    [field: SerializeField] public int ItemID { get; private set;}
    [field: SerializeField] public Sprite ItemIcon { get; private set;}
    [field: SerializeField] public GameObject ItemObject { get; private set; }
    [field :TextArea][field: SerializeField] public string ItemInfo { get; private set;}
    [field: SerializeField] public int buyPrice { get; private set;}
    [field: SerializeField] public int sellPrice { get; private set;}
    [field: SerializeField] public bool isStackable { get; private set;}
    [field: SerializeField] public int maxStack { get; private set;}
    [field: SerializeField] public bool isEquipment { get; private set; }
    [field: SerializeField] public bool isConsumable { get; private set; }
    [field: SerializeField] public PlayerSO itemData { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // 인벤토리에 넣고 삭제하기
            Inventory.Instance.AddItem(this);
            Destroy(gameObject);            
        }
    }
}
