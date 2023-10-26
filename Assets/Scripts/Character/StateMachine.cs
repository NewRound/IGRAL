using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public PlayerController PlayerController { get; private set; }

    public MovementState MovementState { get; private set; }
    
    public StateMachine(PlayerController playerController)
    {
        PlayerController = playerController;

        MovementState = new MovementState(this);
    }

    public void Init()
    {
        ChangeState(MovementState);
    }


    public void ChangeState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState.UpdateState();
    }

    public void PhysicsUpdate()
    {
        _currentState.PhysicsUpdateState();
    }
}
