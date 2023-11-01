using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : IState
{
    [Header("Animation")]
    protected AnimationController animationController;
    protected CharacterAnimationsData animationsData;

    public abstract void Enter();

    public abstract void Exit();

    public abstract void UpdateState();

    public abstract void PhysicsUpdateState();

    public virtual void OnDead()
    {

    }
}
