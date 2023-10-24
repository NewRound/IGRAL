using UnityEngine;

public class ItemBaseSO : ScriptableObject
{
    [field: Header("# Base Info")]
    [field: SerializeField] public string ItemName { get; private set;}
    [field: SerializeField] public int ItemID { get; private set;}
    [field: SerializeField] public Sprite ItemIcon { get; private set;}
    [field: SerializeField] public GameObject ItemObject { get; private set; }
    [field :TextArea][field: SerializeField] public string ItemInfo { get; private set;}
    [field: SerializeField] public int buyPrice { get; private set;}
    [field: SerializeField] public int sellPrice { get; private set;} 






}
