using UnityEngine;

public enum RecoveryType
{
    Health
    , Kcal
}

public class PickupRecovery : MonoBehaviour
{
    [SerializeField] private LayerMask canBePickupBy;
    [SerializeField] private RecoveryType recoveryType;
    [SerializeField] private float amount;

    private void OnTriggerEnter(Collider other)
    {
        if (canBePickupBy.value == (canBePickupBy.value | (1 << other.gameObject.layer)))
        {
            switch(recoveryType)
            {
                case RecoveryType.Health:
                    GameManager.Instance.StatHandler.Recovery(amount);
                    break;
                case RecoveryType.Kcal:
                    GameManager.Instance.StatHandler.RecoveryKcal(amount);
                    break;
            }
        }
    }
}
