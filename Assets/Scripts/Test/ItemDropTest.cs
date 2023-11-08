using System.Collections.Generic;
using UnityEngine;

public class ItemDropTest : MonoBehaviour
{
    [SerializeField] List<GameObject> itemPos;

    private void Start()
    {
        Invoke("ItemDrop", 1f);
    }

    private void ItemDrop()
    {
        foreach(GameObject item in itemPos)
        {
            ItemManager.Instance.RandomDropItem(item.transform.position, ItemType.Artifact);
        }
    }
}
