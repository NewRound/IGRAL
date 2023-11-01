using UnityEngine;

public enum ItemType { Weapon = 1, Artifact, Consumable, Ingredient }
public enum Rarity { Normal = 1, Rare, Unique, Epic }

public class Item : PickupObject
{
    [field: Header("# Base Info")]    
    [field: SerializeField] public string ItemName { get; private set;}
    [field: SerializeField] public int ItemID { get; private set; }
    [field: SerializeField] public ItemType ItemType { get; private set; }
    [field: SerializeField] public Rarity ItemRarity { get; private set; }
    [field: SerializeField] public float DropProbability { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set;}
    [field: SerializeField] public GameObject ItemObject { get; private set; }
    [field :TextArea][field: SerializeField] public string ItemInfo { get; private set;}
    [field: SerializeField] public int Price { get; private set;}
    [field: SerializeField] public bool IsStackable { get; private set;}
    [field: SerializeField] public int MaxStack { get; private set;}
    [field: SerializeField] public PlayerSO itemData { get; private set; }

    public override void Pickup()
    {
        base.Pickup();
        gameObject.SetActive(false);
    }

    public void OnUseItem()
    {
        if (ItemType != ItemType.Consumable) return;     
        
        // 아이템별로 스크립트 만들기???

        // 체력회복
        // 플레이어의 체력을 itemData의 health 비율 만큼 회복....

        // 아드레날린
        // 플레이어의 공격속도와 이동속도를 증가
        // itemdata 의 attackdealy 는 감소 , speed 증가
        // player.data.attackdelay -= itemdata.attackdelay
        
        // 슈류탄
        // 전방에 투척, 일정 범위의 적들에게 데미지
        // OnTriggerEnter?
        // collider.other.getcomponent<T>.takeDamage(this.Itemdata.Attack)
        
    }


}
