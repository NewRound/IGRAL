using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private IItem[] itemArray;

    public IItem[] GetItemArray()
    {
        return itemArray;
    }
}
