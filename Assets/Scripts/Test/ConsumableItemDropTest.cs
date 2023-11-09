using UnityEngine;

public class ConsumableItemDropTest : MonoBehaviour
{

    private void Start()
    {
        Invoke("ItemDrop", 1f);
    }

    private void ItemDrop()
    {

        ItemManager.Instance.RandomDropItem(transform.position, ItemType.Consumable);

    }
}
