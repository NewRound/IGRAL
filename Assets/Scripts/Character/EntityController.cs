using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    [field: SerializeField] public GroundData GroundData { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        
    }
}
