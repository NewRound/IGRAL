using System;
using UnityEngine;

public class TraceDataHandler
{
    public bool IsTracing { get; private set; }
    private TraceData _traceData;
    private Transform _myTrans;
    private Transform _playerTrans;

    private float _missingTargetElpasedTime;

    public TraceDataHandler(TraceData traceData, Transform myTrans, Transform playerTrans)
    {
        _traceData = traceData;
        _myTrans = myTrans;
        _playerTrans = playerTrans;
    }

    public void CalculateDistance()
    {
        float distance = Vector3.Distance(_myTrans.position, _playerTrans.position);
        if (!IsTracing)
        {
            if (distance < _traceData.TracingMinDistance)
                IsTracing = true;
        }
        else
        {
            if (distance > _traceData.TracingMaxDistance)
            {
                CalculateMissingTargetElapsedTime();
            }
        }
        
    }

    private void CalculateMissingTargetElapsedTime()
    {
        _missingTargetElpasedTime += Time.deltaTime;

        if (_missingTargetElpasedTime > _traceData.TracingEndTime)
        {
            IsTracing = false;
            _missingTargetElpasedTime = 0f;
        }
    }
}