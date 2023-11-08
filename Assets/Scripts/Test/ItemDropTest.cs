using UnityEngine;

public class ItemDropTest : MonoBehaviour
{
    private void Start()
    {
        Invoke("ItemDrop", 1f);
    }

    private void ItemDrop()
    {
        //현재는 아티팩트만 구현되어 있음
        ItemManager.Instance.RandomDropItem(transform.position, ItemType.Artifact);
    }

}
