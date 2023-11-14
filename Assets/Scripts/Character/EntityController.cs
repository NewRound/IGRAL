using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    [SerializeField] protected SkinnedMeshRenderer meshRenderer;

    protected Material myMaterial;

    [field: SerializeField] public GroundData GroundData { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        myMaterial = meshRenderer.material;


    }
}
