using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectViewer
{
    private EffectData _data;
    public EffectViewer(EffectData data)
    {
        _data = data;
    }

    public IEnumerator ShowWeaponEffectGradually(List<GameObject> goList)
    {
        _data.DissolveMaterial.DOFloat(1, _data.SplitValue, _data.DissolveDuration);

        foreach (GameObject go in goList)
        {
            go.SetActive(true);
        }

        ActivateAura(true);
        yield return CoroutineRef.GetWaitForSeconds(_data.AuraDuration);
        ActivateAura(false);
    }

    public IEnumerator ShowWeaponEffectWithoutDissolve(List<GameObject> goList)
    {
        foreach (GameObject go in goList)
        {
            go.SetActive(true);
        }

        ActivateAura(true);
        yield return CoroutineRef.GetWaitForSeconds(_data.AuraDuration);
        ActivateAura(false);
    }

    public IEnumerator ConcealWeaponEffectGradually(List<GameObject> goList)
    {
        _data.DissolveMaterial.DOFloat(0, _data.SplitValue, _data.DissolveDuration);

        ActivateAura(true);
        yield return CoroutineRef.GetWaitForSeconds(_data.AuraDuration);
        ActivateAura(false);

        foreach (GameObject go in goList)
        {
            go.SetActive(false);
        }
    }

    public IEnumerator ConcealWeaponEffectWithoutDissolve(List<GameObject> goList)
    {
        ActivateAura(true);
        yield return CoroutineRef.GetWaitForSeconds(_data.AuraDuration);
        ActivateAura(false);

        foreach (GameObject go in goList)
        {
            go.SetActive(false);
        }
    }

    public void ResetData()
    {
        _data.DissolveMaterial.SetFloat(_data.SplitValue, 0);
        _data.AuraMaterial.SetFloat(_data.ActiveProperty, 0);
    }

    private void ActivateAura(bool isActive)
    {
        float activeFloat = isActive ? 1 : 0;
        _data.AuraMaterial.SetFloat(_data.ActiveProperty, activeFloat);
    }

    
}