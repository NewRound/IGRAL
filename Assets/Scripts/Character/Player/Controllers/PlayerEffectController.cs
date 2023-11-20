using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    private IEnumerator _currentEnumerator;

    [field: SerializeField] public EffectDataHandler EffectDataHandler { get; private set; }
    private EffectViewer _effectViewer;

    private void Awake()
    {
        _effectViewer = new EffectViewer(EffectDataHandler.EffectData);
    }

    public void AppearWeapon(List<GameObject> goList)
    {
        CheckCurrentWeaponEffect(goList , true);

        if (_currentEnumerator != null)
        {
            StartCoroutine(_currentEnumerator);
        }
    }

    public void DisappearWeapon(List<GameObject> goList)
    {
        CheckCurrentWeaponEffect(goList, false);

        if (_currentEnumerator != null)
        {
            StartCoroutine(_currentEnumerator);
        }
    }

    public void AppearWeaponWithoutDissolve(List<GameObject> goList)
    {
        CheckCurrentWeaponEffectWithoutDissolve(goList, true);

        if (_currentEnumerator != null)
        {
            StartCoroutine(_currentEnumerator);
        }
    }

    public void DisappearWeaponWithoutDissolve(List<GameObject> goList)
    {
        CheckCurrentWeaponEffectWithoutDissolve(goList, false);

        if (_currentEnumerator != null)
        {
            StartCoroutine(_currentEnumerator);
        }
    }

    private void CheckCurrentWeaponEffect(List<GameObject> goList, bool isActive)
    {
        if (_currentEnumerator != null)
        {
            StopCoroutine(_currentEnumerator);
        }

        _currentEnumerator = isActive ? 
            _effectViewer.ShowWeaponEffectGradually(goList) : 
            _effectViewer.ConcealWeaponEffectGradually(goList);
    }

    private void CheckCurrentWeaponEffectWithoutDissolve(List<GameObject> goList, bool isActive)
    {
        if (_currentEnumerator != null)
        {
            StopCoroutine(_currentEnumerator);
        }

        _currentEnumerator = isActive ?
            _effectViewer.ShowWeaponEffectWithoutDissolve(goList) :
            _effectViewer.ConcealWeaponEffectWithoutDissolve(goList);
    }

    public void ResetViewerData()
    {
        _effectViewer.ResetData();
    }
}
