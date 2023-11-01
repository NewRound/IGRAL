using System.Collections.Generic;
using UnityEngine;

public class ItemManager : CustomSingleton<ItemManager>
{    
    [SerializeField] private List<Item> items;
    private Dictionary<Rarity, List<Item>> ItemsByRarity = new Dictionary<Rarity, List<Item>>();

    public GameObject pickupItem { get; private set; }

    private void Start()
    {
        items = Resources.Load<ItemList>("Items/ItemList").GetItemList();      
        
        // ����� Ű������ �Ͽ� ��޺��� ������ ����Ʈ ����
        foreach (Item item in items)
        {
            if(!ItemsByRarity.ContainsKey(item.ItemRarity))
            {
                ItemsByRarity[item.ItemRarity] = new List<Item>();
            }
            ItemsByRarity[item.ItemRarity].Add(item);
        }        
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
        Item dropItem = RandomSelectItem();

        // ��� Ȯ���� ���� ����
        float dropChance = Random.value;
        if (dropItem.DropProbability > dropChance)
        {
            Instantiate(dropItem.ItemObject, dropPos, Quaternion.identity, transform);
        }
        else return;
    }

    private Item RandomSelectItem()
    {
        Item selectedItem = null;
        
        // Ȯ���� ���� ��� ����
        Rarity selectRarity = GetRarityWeight();

        List<Item> itemsOfSelect;

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
