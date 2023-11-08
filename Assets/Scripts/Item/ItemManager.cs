using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Artifact, Consumable, Ingredient, Recovery }
public enum Rarity { Normal, Rare, Unique, Epic }

public class ItemManager : CustomSingleton<ItemManager>
{
    public GameObject pickupItem { get; private set; }
    private GameObject _itemBox;

    private Dictionary<Rarity, List<Item>> _itemsArtifactByRarity = new Dictionary<Rarity, List<Item>>();
    private Dictionary<Rarity, List<Item>> _itemsConsumableByRarity = new Dictionary<Rarity, List<Item>>();
    private Dictionary<Rarity, List<Item>> _itemsIngredientByRarity = new Dictionary<Rarity, List<Item>>();
    private Dictionary<Rarity, List<Item>> _itemsWeaponByRarity = new Dictionary<Rarity, List<Item>>();

    // 기본 드랍 확율 Normal 55, Rare 30, Unique 10, Epic 5
    private int[] rarityProbability = { 55, 30, 10, 5 };

    private void Awake()
    {
        _itemBox = Resources.Load<GameObject>("Items/ItemBox");
        ItemList items = Resources.Load<ItemList>("Items/ItemList");

        // 등급을 키값으로 하여 등급별로 아이템 리스트 생성
        // itemArtifact
        for (int i = 0; i < items.itemArtifact.Length; i++)
        {
            if (!_itemsArtifactByRarity.ContainsKey(items.itemArtifact[i].ItemRarity))
            {
                _itemsArtifactByRarity[items.itemArtifact[i].ItemRarity] = new List<Item>();
            }
            _itemsArtifactByRarity[items.itemArtifact[i].ItemRarity].Add(items.itemArtifact[i]);
        }

        // itemConsumable
        for (int i = 0; i < items.itemConsumable.Length; i++)
        {
            if (!_itemsConsumableByRarity.ContainsKey(items.itemConsumable[i].ItemRarity))
            {
                _itemsConsumableByRarity[items.itemConsumable[i].ItemRarity] = new List<Item>();
            }
            _itemsConsumableByRarity[items.itemConsumable[i].ItemRarity].Add(items.itemConsumable[i]);
        }

        // itemIngredient
        for (int i = 0; i < items.itemIngredient.Length; i++)
        {
            if (!_itemsIngredientByRarity.ContainsKey(items.itemIngredient[i].ItemRarity))
            {
                _itemsIngredientByRarity[items.itemIngredient[i].ItemRarity] = new List<Item>();
            }
            _itemsIngredientByRarity[items.itemIngredient[i].ItemRarity].Add(items.itemIngredient[i]);
        }

        // itemWeapons
        for (int i = 0; i < items.itemWeapons.Length; i++)
        {
            if (!_itemsWeaponByRarity.ContainsKey(items.itemWeapons[i].ItemRarity))
            {
                _itemsWeaponByRarity[items.itemWeapons[i].ItemRarity] = new List<Item>();
            }
            _itemsWeaponByRarity[items.itemWeapons[i].ItemRarity].Add(items.itemWeapons[i]);
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


    public void RandomDropItem(Vector3 dropPos, ItemType itemType)
    {
        int targetRarity = Probability.Drop(rarityProbability);

        List<Item> items = new List<Item>();

        switch (itemType)
        {
            case ItemType.Artifact:
                items = _itemsArtifactByRarity[(Rarity)targetRarity];
                break;
            case ItemType.Consumable:
                items = _itemsArtifactByRarity[(Rarity)targetRarity];
                break;
            case ItemType.Ingredient:
                items = _itemsArtifactByRarity[(Rarity)targetRarity];
                break;
            case ItemType.Weapon:
                items = _itemsArtifactByRarity[(Rarity)targetRarity];
                break;
        }

        if(items.Count > 0)
        {
            int[] probability = new int[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                probability[i] = items[i].GetComponent<Item>().DropProbability;
            }

            int target = Probability.Drop(probability);
            GameObject itemBox = Instantiate(_itemBox);
            switch (itemType)
            {
                case ItemType.Artifact:
                    itemBox.AddComponent<ItemArtifact>().DropSet(items[target]);
                    break;
                case ItemType.Consumable:
                    itemBox.AddComponent<ItemConsumable>().DropSet(items[target]);
                    break;
                case ItemType.Ingredient:
                    itemBox.AddComponent<ItemIngredient>().DropSet(items[target]);
                    break;
                case ItemType.Weapon:
                    itemBox.AddComponent<ItemWeapon>().DropSet(items[target]);
                    break;
            }

            itemBox.transform.position = dropPos;
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