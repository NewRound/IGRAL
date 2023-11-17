using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    [Header("Dissolve")]
    private Material _dissolveMaterial;
    [SerializeField] private float dissolveDuration = 0.5f;
    [SerializeField] private string splitValue = "_SplitValue";

    [Header("Aura")]
    private Material _auraMaterial;
    [SerializeField] private float auraDuration = 1f;
    [SerializeField] private string activeProperty = "_ActiveFloat";

    private IEnumerator _currentEnumerator;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AppearWeapon();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DisappearWeapon();
        }
    }



    public void SetDissolveMaterial(Material material)
    {
        _dissolveMaterial = material;
    }

    public void SetAuraMaterial(Material material)
    {
        _auraMaterial = material;
    }

    public void AppearWeapon()
    {
        if (_currentEnumerator != null)
        {
            StopCoroutine(_currentEnumerator);
        }

        _currentEnumerator = ShowWeaponEffectGradually();

        _dissolveMaterial.DOFloat(1, splitValue, dissolveDuration);
        StartCoroutine(_currentEnumerator);
    }

    public void DisappearWeapon()
    {
        CheckCurrentWeaponEffectEnumeration();

        _dissolveMaterial.DOFloat(0, splitValue, dissolveDuration);
        StartCoroutine(_currentEnumerator);
    }

    private void CheckCurrentWeaponEffectEnumeration()
    {
        if (_currentEnumerator != null)
        {
            StopCoroutine(_currentEnumerator);
        }

        _currentEnumerator = ShowWeaponEffectGradually();
    }

    private void ActivateAura(bool isActive)
    {
        float activeFloat = isActive ? 1 : 0;
        _auraMaterial.SetFloat(activeProperty, activeFloat);
    }

    private IEnumerator ShowWeaponEffectGradually()
    {
        ActivateAura(true);
        yield return CoroutineRef.GetWaitForSeconds(auraDuration);
        ActivateAura(false);
    }
}
