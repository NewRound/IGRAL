using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Artifact, Consumable, Ingredient, Recovery }
public enum Rarity { Normal, Rare, Unique, Epic }

public class ItemManager : CustomSingleton<ItemManager>
{
    public ItemSO itemSO { get; private set; }
    public PickupItem pickupItem { get; private set; }
    public ItemConsumable itemConsumable { get; private set; }
    private PickupItem itemBox;

    private Dictionary<Rarity, List<ItemSO>> _itemsArtifactByRarity = new Dictionary<Rarity, List<ItemSO>>();
    private Dictionary<Rarity, List<ItemConsumable>> _itemsConsumableByRarity = new Dictionary<Rarity, List<ItemConsumable>>();
    private Dictionary<Rarity, List<ItemSO>> _itemsIngredientByRarity = new Dictionary<Rarity, List<ItemSO>>();
    private Dictionary<Rarity, List<ItemSO>> _itemsWeaponByRarity = new Dictionary<Rarity, List<ItemSO>>();

    // 기본 드랍 확율 Normal 55, Rare 30, Unique 10, Epic 5
    private int[] rarityProbability = { 55, 30, 10, 5 };

    private void Awake()
    {
        itemBox = Resources.Load<PickupItem>("Items/ItemBox");
        ItemList items = Resources.Load<ItemList>("Items/ItemList");

        // 등급을 키값으로 하여 등급별로 아이템 리스트 생성
        // itemArtifact
        for (int i = 0; i < items.itemArtifact.Length; i++)
        {
            if (!_itemsArtifactByRarity.ContainsKey(items.itemArtifact[i].ItemRarity))
            {
                _itemsArtifactByRarity[items.itemArtifact[i].ItemRarity] = new List<ItemSO>();
            }
            _itemsArtifactByRarity[items.itemArtifact[i].ItemRarity].Add(items.itemArtifact[i]);
        }

        // itemConsumable
        for (int i = 0; i < items.itemConsumable.Length; i++)
        {
            if (!_itemsConsumableByRarity.ContainsKey(items.itemConsumable[i].item.ItemRarity))
            {
                _itemsConsumableByRarity[items.itemConsumable[i].item.ItemRarity] = new List<ItemConsumable>();
            }
            _itemsConsumableByRarity[items.itemConsumable[i].item.ItemRarity].Add(items.itemConsumable[i]);
        }

        // itemIngredient
        for (int i = 0; i < items.itemIngredient.Length; i++)
        {
            if (!_itemsIngredientByRarity.ContainsKey(items.itemIngredient[i].ItemRarity))
            {
                _itemsIngredientByRarity[items.itemIngredient[i].ItemRarity] = new List<ItemSO>();
            }
            _itemsIngredientByRarity[items.itemIngredient[i].ItemRarity].Add(items.itemIngredient[i]);
        }

        // itemWeapons
        for (int i = 0; i < items.itemWeapons.Length; i++)
        {
            if (!_itemsWeaponByRarity.ContainsKey(items.itemWeapons[i].ItemRarity))
            {
                _itemsWeaponByRarity[items.itemWeapons[i].ItemRarity] = new List<ItemSO>();
            }
            _itemsWeaponByRarity[items.itemWeapons[i].ItemRarity].Add(items.itemWeapons[i]);
        }
    }

    public void SetPickupItem(ItemSO itemData, PickupItem pickup)
    {
        itemSO = itemData;
        pickupItem = pickup;
    }
    
    public void SetConsumable(ItemConsumable itemData)
    {
        itemConsumable = itemData;
    }


    public void DelSetPickupItem()
    {
        itemSO = null;
        pickupItem = null;
    }

    public void DelConsumable()
    {
        itemConsumable = null;
    }


    public void RandomDropItem(Vector3 dropPos, ItemType itemType)
    {
        int targetRarity = Probability.Drop(rarityProbability);

        switch (itemType)
        {
            case ItemType.Artifact:
                List<ItemSO> itemArtifact = _itemsArtifactByRarity[(Rarity)targetRarity];
                if (itemArtifact.Count > 0)
                {
                    int[] probability = new int[itemArtifact.Count];
                    for (int i = 0; i < itemArtifact.Count; i++)
                    {
                        probability[i] = itemArtifact[i].DropProbability;
                    }

                    int target = Probability.Drop(probability);
                    PickupItem pickupItem = Instantiate(itemBox);
                    pickupItem.DropItemSet(itemArtifact[target]);
                    pickupItem.transform.position = dropPos;
                }
                break;
            case ItemType.Consumable:
                List<ItemConsumable> itemConsumable = _itemsConsumableByRarity[(Rarity)targetRarity];
                if (itemConsumable.Count > 0)
                {
                    int[] probability = new int[itemConsumable.Count];
                    for (int i = 0; i < itemConsumable.Count; i++)
                    {
                        probability[i] = itemConsumable[i].item.DropProbability;
                    }

                    int target = Probability.Drop(probability);
                    PickupItem pickupItem = Instantiate(itemBox);
                    pickupItem.DropConsumableItemSet(itemConsumable[target]);
                    pickupItem.transform.position = dropPos;
                }
                break;
            case ItemType.Ingredient:
                // TODO 구현 필요
                break;
            case ItemType.Weapon:
                // TODO 구현 필요
                break;
        }


    }

}

public static class Probability
{
    public static int Drop(int[] probability)
    {
        int max = 1;
        for (int i = 0; i < probability.Length; i++)
        {
            max += probability[i];
        }
        int ran = Random.Range(0, max);
        int cumulative = 0;
        int target = -1;
        for (int i = 0; i < probability.Length; i++)
        {
            cumulative += probability[i];
            if (ran <= cumulative)
            {
                target = i;
                break;
            }
        }

        return target;
    }
}