using UnityEngine;

public class ItemIngredient : Item
{
    public override void Pickup()
    {
        base.Pickup();
        //TODO ��� �������� �浹�� �ٷ� �߰��Ǵ� �Լ� ���� �ʿ�
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            Pickup();
        }
    }
}
