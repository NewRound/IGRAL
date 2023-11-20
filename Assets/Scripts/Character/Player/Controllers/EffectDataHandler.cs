using System;
using UnityEngine;

[Serializable]
public class EffectDataHandler
{
    [field: SerializeField] public EffectData EffectData { get; private set; }

    public void SetDissolveMaterial(Material material)
    {
        EffectData.DissolveMaterial = material;
    }

    public void SetAuraMaterial(Material material)
    {
        EffectData.AuraMaterial = material;
    }
}