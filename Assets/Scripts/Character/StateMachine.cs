using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;

    [field: Header("Jump")]
    public JumpCountHandler JumpCountHandler { get; protected set; }

    [field: Header("Ground")]
    public GroundDataHandler GroundDataHandler { get; protected set; }

    public abstract void Init();

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public virtual void Update()
    {
        currentState.UpdateState();
    }

    public virtual void PhysicsUpdate()
    {
        GroundDataHandler.CheckIsGrounded();
        currentState.PhysicsUpdateState();
    }

    protected abstract void CheckGround();
}
