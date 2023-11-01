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
        
        // 등급을 키값으로 하여 등급별로 아이템 리스트 생성
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
        // 랜덤 아이템 선택
        Item dropItem = RandomSelectItem();

        // 드랍 확률에 따라 생성
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
        
        // 확률에 따른 등급 선택
        Rarity selectRarity = GetRarityWeight();

        List<Item> itemsOfSelect;

        // 등급에 해당하는 리스트(itemsOfSelect)를 저장(out)
        if (ItemsByRarity.TryGetValue(selectRarity, out itemsOfSelect))
        {
            if(itemsOfSelect.Count > 0)
            {
                int randomIndex = Random.Range(0, itemsOfSelect.Count);
                selectedItem = itemsOfSelect[randomIndex];
                // 리스트 중에서 하나의 아이템 선택
            }
        }
        return selectedItem;        
    }

    private Rarity GetRarityWeight()
    {
        // 등급에 따른 확률 설정
        float RareChance = 0.2f;
        float UniqeChance = 0.05f;
        float EpicChance = 0.01f;

        float RarityChance = Random.value;

        // 확률에 따른 열거형 값 반환
        if (RarityChance <= EpicChance) return Rarity.Epic;
        else if (RarityChance <= UniqeChance) return Rarity.Unique;
        else if (RarityChance <= RareChance) return Rarity.Rare;
        else return Rarity.Normal;
    }
}
