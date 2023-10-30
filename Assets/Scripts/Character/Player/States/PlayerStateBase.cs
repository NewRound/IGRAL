using UnityEngine;

public abstract class PlayerStateBase : IState
{
    [Header("Player")]
    protected StateMachine stateMachine;
    protected PlayerController playerController;
    protected MovementDataHandler movementDataHandler;

    [Header("Animation")]
    protected AnimationController animationController;
    protected PlayerAnimationsData animationsData;

    [Header("Input")]
    protected PlayerInputAction inputActions;

    public PlayerStateBase(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        playerController = stateMachine.PlayerController;

        movementDataHandler = stateMachine.MovementDataHandler;

        animationController = playerController.AnimationController;
        animationsData = playerController.AnimationController.AnimationData;

        InitInputActions();
    }

    public void OnMoveInput(Vector2 direction)
    {
        stateMachine.MovementDataHandler.SetDirection(direction);
    }

    public abstract void Enter();

    public abstract void Exit();

    public virtual void UpdateState()
    {
        stateMachine.MovementDataHandler.UpdateSpeed();
        stateMachine.MovementDataHandler.Look();
    }

    public virtual void PhysicsUpdateState()
    {
        stateMachine.MovementDataHandler.Move();
    }

    public virtual void OnDead()
    {
        playerController.MoveAction -= OnMoveInput;
    }

    private void InitInputActions()
    {
        inputActions = playerController.InputActions;
        
        playerController.MoveAction += OnMoveInput;
    }

    

    
}
