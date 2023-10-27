using System.Collections.Generic;
using UnityEngine;

public class ItemManager : CustomSingleton<ItemManager>
{    
    [SerializeField] private List<Item> items;
    [SerializeField] private Dictionary<Rarity, List<Item>> ItemsByRarity 
        = new Dictionary<Rarity, List<Item>>();

    private void Start()
    {
        foreach (Item item in items)
        {
            if(!ItemsByRarity.ContainsKey(item.ItemRarity))
            {
                ItemsByRarity[item.ItemRarity] = new List<Item>();
            }
            ItemsByRarity[item.ItemRarity].Add(item);
        }
        // 등급을 키값으로 하여 등급별로 아이템 리스트 생성
    }

    public void RandomDropItem(Transform dropPos)
    {
        // 랜덤 아이템 선택
        Item dropItem = RandomSelectItem();

        // 드랍 확률에 따라 생성
        float chance = Random.value;
        Debug.Log(chance);
        if (dropItem.DropProbability > chance)
        {
            Instantiate(dropItem.ItemObject, dropPos);
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
        if (ItemsByRarity.TryGetValue(selectRarity,out itemsOfSelect))
        {
            if(itemsOfSelect.Count > 0)
            {
                int randomIndex = Random.Range(0, itemsOfSelect.Count);
                selectedItem = itemsOfSelect[randomIndex];
                Debug.Log(selectedItem.ItemName);
                Debug.Log(selectedItem.ItemRarity);
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
