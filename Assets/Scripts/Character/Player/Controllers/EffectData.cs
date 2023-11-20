using System;
using UnityEngine;

[Serializable]
public class EffectData
{
    [field: Header("Dissolve")]
    [field: SerializeField] public float DissolveDuration { get; private set; } = 0.5f;
    [field: SerializeField] public string SplitValue { get; private set; } = "_SplitValue";
    public Material DissolveMaterial { get; set; }

    [field: Header("Aura")]
    [field: SerializeField] public float AuraDuration { get; private set; } = 1f;
    [field: SerializeField] public string ActiveProperty { get; private set; } = "_ActiveFloat";
    public Material AuraMaterial { get; set; }

}