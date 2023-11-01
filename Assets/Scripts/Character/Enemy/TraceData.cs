using System;
using UnityEngine;

[Serializable]
public class TraceData
{
    // 추적 시작 거리 추적 종료 거리 추적 종료 시간 RayOffsetX 
    [field: SerializeField] public float TracingMinDistance { get; private set; } = 5f;
    [field: SerializeField] public float TracingMaxDistance { get; private set; } = 10f;
    [field: SerializeField] public float TracingEndTime { get; private set; } = 3f;
    [field: SerializeField] public float FrontRayOffsetXPos { get; private set; } = 0.5f;
    [field: SerializeField] public float WatingTimeMax { get; private set; } = 2f;
}