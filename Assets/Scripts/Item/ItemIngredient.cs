using UnityEngine;

public class ItemIngredient : Item
{
    public override void Pickup()
    {
        base.Pickup();
        //TODO 재료 아이템은 충돌시 바로 추가되는 함수 구현 필요
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            Pickup();
        }
    }
}
