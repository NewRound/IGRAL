using UnityEngine;

public enum RecoveryType
{
    Health,
    Kcal,
}

public class RecoveryPoint : MonoBehaviour
{
    [SerializeField] private RecoveryType recoveryType;
    [SerializeField] private float recoveryAmount;
    [SerializeField] private float recoveryDelay;

    private bool _isReady = false;
    private float curTime = 0;

    private void Update()
    {
        if(!_isReady)
        {
            curTime += Time.deltaTime;

            if (curTime > recoveryDelay)
            {
                curTime = 0;
                _isReady = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isReady)
        {
            if (other.TryGetComponent<InputController>(out InputController inputController))
            {
                switch (recoveryType)
                {
                    case RecoveryType.Health:
                        inputController.StatHandler.Recovery(recoveryAmount);
                        break;
                    case RecoveryType.Kcal:
                        inputController.StatHandler.RecoveryKcal(recoveryAmount);
                        break;
                    default:
                        break;
                }
                _isReady = false;
            }
        }
    }
}
