using UnityEngine;

public abstract class PlayerStateBase : StateBase
{
    [Header("Player")]
    protected PlayerStateMachine stateMachine;
    protected InputController InputController;
    protected PlayerMovementDataHandler movementDataHandler;

    [Header("Input")]
    protected PlayerInputAction inputActions;

    public PlayerStateBase(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        InputController = stateMachine.InputController;

        movementDataHandler = stateMachine.MovementDataHandler;

        animationController = InputController.AnimationController;
        animationsData = InputController.AnimationController.AnimationData;

        InitInputActions();
    }

    public void OnMoveInput(Vector2 direction)
    {
        stateMachine.MovementDataHandler.SetDirection(direction);
    }

    public override void UpdateState()
    {
        stateMachine.MovementDataHandler.UpdateSpeed();
        stateMachine.MovementDataHandler.Look();
    }

    public override void PhysicsUpdateState()
    {
        stateMachine.MovementDataHandler.Move();
    }

    public override void OnDead()
    {
        InputController.MoveAction -= OnMoveInput;
    }

    private void InitInputActions()
    {
        inputActions = InputController.InputActions;
        
        InputController.MoveAction += OnMoveInput;
    }
}
