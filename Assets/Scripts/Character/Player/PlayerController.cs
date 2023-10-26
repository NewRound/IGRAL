using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : InputController
{
    [SerializeField] private PlayerSO stat;

    public PlayerStatHandler StatHandler { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Transform Transform { get; private set; }

    // TODO : StateMachine 들어갈 자리
    public PlayerStateBase PlayerStateBase { get; private set; }

    [field: SerializeField] public Movement Movement { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StatHandler = new PlayerStatHandler(stat);
        Rigidbody = GetComponent<Rigidbody>();
        Transform = transform;
        PlayerStateBase = new PlayerStateBase(this, Movement);
    }

    private void Start()
    {
    }

    private void Update()
    {
        PlayerStateBase.UpdateState();
    }

    private void FixedUpdate()
    {
        PlayerStateBase.PhysicsUpdateState();
    }

}

[Serializable]
public class Movement
{
    [Header("Acceleration")]
    public float acceleratingTime = 1f;

    [Header("Rotation")]
    public float rotationSpeed = 10f;
    public float minAbsAngle = 90f;
    public float maxAbsAngle = 270f;
}