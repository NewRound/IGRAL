using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Artifact, Consumable, Ingredient, Recovery }
public enum Rarity { Normal, Rare, Unique, Epic }

public class ItemManager : CustomSingleton<ItemManager>
{    
    [SerializeField] private IItem[] items;
    private Dictionary<Rarity, List<IItem>> ItemsByRarity = new Dictionary<Rarity, List<IItem>>();

    public GameObject pickupItem { get; private set; }

    private void Start()
    {
/*        items = Resources.Load<ItemList>("Items/ItemList").GetItemArray();      
        
        // ����� Ű������ �Ͽ� ��޺��� ������ ����Ʈ ����      

        for(int i =0; i < items.Length; i++)
        {
            if (!ItemsByRarity.ContainsKey(items[i].ItemRarity))
            {
                ItemsByRarity[items[i].ItemRarity] = new List<IItem>();
            }
            ItemsByRarity[items[i].ItemRarity].Add(items[i]);
        }*/
    }

    public void SetPickupItem(GameObject go)
    {
        pickupItem = go;
    }

    public void DelSetPickupItem()
    {
        pickupItem = null;
    }

    public void RandomDropItem(Vector3 dropPos)
    {
        // ���� ������ ����
        IItem dropItem = RandomSelectItem();

        // ��� Ȯ���� ���� ����
        float dropChance = Random.value;
        if (dropItem.DropProbability > dropChance)
        {
            Instantiate(dropItem.ItemObject, dropPos, Quaternion.identity, transform);
        }
        else return;
    }

    private IItem RandomSelectItem()
    {
        IItem selectedItem = null;
        
        // Ȯ���� ���� ��� ����
        Rarity selectRarity = GetRarityWeight();

        List<IItem> itemsOfSelect;

        // ��޿� �ش��ϴ� ����Ʈ(itemsOfSelect)�� ����(out)
        if (ItemsByRarity.TryGetValue(selectRarity, out itemsOfSelect))
        {
            if(itemsOfSelect.Count > 0)
            {
                int randomIndex = Random.Range(0, itemsOfSelect.Count);
                selectedItem = itemsOfSelect[randomIndex];
                // ����Ʈ �߿��� �ϳ��� ������ ����
            }
        }
        return selectedItem;        
    }

    private Rarity GetRarityWeight()
    {
        // ��޿� ���� Ȯ�� ����
        float RareChance = 0.2f;
        float UniqeChance = 0.05f;
        float EpicChance = 0.01f;

        float RarityChance = Random.value;

        // Ȯ���� ���� ������ �� ��ȯ
        if (RarityChance <= EpicChance) return Rarity.Epic;
        else if (RarityChance <= UniqeChance) return Rarity.Unique;
        else if (RarityChance <= RareChance) return Rarity.Rare;
        else return Rarity.Normal;
    }
}
