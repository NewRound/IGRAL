using UnityEngine;

public class PickupIngredient : MonoBehaviour
{
    [SerializeField] private LayerMask canBePickupBy;

    public virtual void Pickup()
    {
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
