using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEffectController
{
    private Material dissolveMaterial;
    private Material auraMaterial;

    private string activeProperty = "_ActiveFloat";

    public void SetDissolveMaterial(Material material)
    {
        dissolveMaterial = material;
    }

    public void SetAuraMaterial(Material material)
    {
        auraMaterial = material;
    }

    public void ActiveAura(bool isActive)
    {
        float activeFloat = isActive ? 1 : 0;
        auraMaterial.SetFloat(activeProperty, activeFloat);
    }
}
