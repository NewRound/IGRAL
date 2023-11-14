using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    [SerializeField] protected SkinnedMeshRenderer meshRenderer;

    [Header("Blink")]
    [SerializeField] private float blinkDuration = 0.2f;
    private IEnumerator _blinkCoroutine;

    protected Material myMaterial;

    [field: SerializeField] public GroundData GroundData { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        myMaterial = meshRenderer.material;
    }

    public virtual void OnDamaged()
    {
        if (_blinkCoroutine == null)
        {
            _blinkCoroutine = Blink();
        }
        else
        {
            StopCoroutine(_blinkCoroutine);
            myMaterial.color = Color.white;
        }

        StartCoroutine(_blinkCoroutine);
    }

    private IEnumerator Blink()
    {
        myMaterial.DOColor(Color.red, blinkDuration);
        yield return new WaitForSeconds(blinkDuration);
        myMaterial.DOColor(Color.white, blinkDuration);
        yield return new WaitForSeconds(blinkDuration);
    }
}
