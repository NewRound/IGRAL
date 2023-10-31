using System;
using UnityEngine;

public class GroundDataHandler
{
    [Header("GroundData")]
    private GroundData _groundData;
    private float _checkingRadius;

    public GroundDataHandler(GroundData data)
    {
        _groundData = data;
        _checkingRadius = _groundData.GroundYOffset * _groundData.GroundRadiusMod;
    }

    private Transform _controllerTrans;

    public bool IsGrounded { get; private set; }

    public void Init(Transform controllerTrans)
    {
        _controllerTrans = controllerTrans;
    }

    public void CheckIsGrounded()
    {
        Vector3 checkOffsetPos = _controllerTrans.position;
        checkOffsetPos.y += _groundData.GroundYOffset;

        IsGrounded = Physics.CheckSphere(
            checkOffsetPos,
            _checkingRadius,
            _groundData.GroundLayer,
            QueryTriggerInteraction.Ignore);
    }
}
