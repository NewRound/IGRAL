using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;


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
        currentState.PhysicsUpdateState();
    }
}
