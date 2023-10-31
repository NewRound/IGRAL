using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class CharacterController : MonoBehaviour
{
    public AnimationController AnimationController { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    [field: SerializeField] public MovementData MovementData { get; private set; }
    [field: SerializeField] public GroundData GroundData { get; private set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        AnimationController = GetComponentInChildren<AnimationController>();
        AnimationController.Init();
    }

}
