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
    private WaitForSeconds _blinkSeconds;

    protected Material myMaterial;


    [field: SerializeField] public GroundData GroundData { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        myMaterial = meshRenderer.materials[0];
    }

    public virtual void OnDamaged()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            myMaterial.color = Color.white;
        }

        _blinkCoroutine = Blink();
        StartCoroutine(_blinkCoroutine);
    }

    private IEnumerator Blink()
    {
        myMaterial.DOColor(Color.red, blinkDuration);

        if (_blinkSeconds == null)
            _blinkSeconds = CoroutineRef.GetWaitForSeconds(blinkDuration);

        yield return _blinkSeconds;
        myMaterial.DOColor(Color.white, blinkDuration);
        yield return _blinkSeconds;
    }
}
