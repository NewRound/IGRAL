using System;
using UnityEngine;

[Serializable]
public class GroundCheck
{
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    [field: SerializeField] public float GroundYOffset { get; private set; } = 0.2f;
    [field: SerializeField] public float GroundRadiusMod { get; private set; } = 1.5f;

    private Transform __playerTrans;

    public void Init(Transform _playerTrans)
    {
        __playerTrans = _playerTrans;
    }

    public bool CheckIsGrounded()
    {
        Vector3 checkOffsetPos = __playerTrans.position;
        checkOffsetPos.y += GroundYOffset;

        return Physics.CheckSphere(
            checkOffsetPos,
            GroundYOffset * GroundRadiusMod,
            GroundLayer,
            QueryTriggerInteraction.Ignore);
    }
}
