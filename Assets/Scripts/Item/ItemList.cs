using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public List<Item> itemList;

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
