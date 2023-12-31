using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private LayerMask canBePickupBy;

    public virtual void Use()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            UIController.Instance.SetInteractiveObject(this.gameObject);
            UIController.Instance.SwitchingInteraction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            UIController.Instance.DelInteractiveObject();
            UIController.Instance.SwitchingAttack();
        }
    }
}
