using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private List<Item> itemList;

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
