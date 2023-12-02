using UnityEngine;

public abstract class PlayerStateBase : StateBase
{
    [Header("Player")]
    protected PlayerStateMachine stateMachine;
    protected InputController InputController;
    protected PlayerAnimationController animationController;
    protected PlayeranimationsData animationsData;

    [Header("Input")]
    protected PlayerInputAction inputActions;

    public PlayerStateBase(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        InputController = stateMachine.InputController;

        animationController = InputController.AnimationController;
        animationsData = InputController.AnimationController.AnimationData;

        InitInputActions();
    }

    public void OnMoveInput(Vector2 direction)
    {
        stateMachine.SetDirection(direction);
    }

    public override void UpdateState()
    {
        stateMachine.UpdateSpeed();
        stateMachine.Look();
    }

    public override void PhysicsUpdateState()
    {
        stateMachine.Move();
    }

    public override void OnDead()
    {
        InputController.MoveAction -= OnMoveInput;
        inputActions.Disable();
        GameManager.Instance.isDie = true;
        UIManager.Instance.OpenUI<UIGameOver>().Open();
    }

    public void InitInputActions()
    {
        inputActions = InputController.InputActions;
        
        InputController.MoveAction += OnMoveInput;
    }
}
