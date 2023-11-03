using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private Item[] itemArray;

    public Item[] GetItemArray()
    {
        return itemArray;
    }
}
