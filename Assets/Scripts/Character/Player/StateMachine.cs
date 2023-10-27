using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public PlayerController PlayerController { get; private set; }

    public PlayerMoveState MovementState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }

    public JumpCountHandler JumpCountSetter { get; private set; }

    [Header("Ground")]
    private GroundCheck _groundCheck;
    public bool IsGrounded { get; private set; }

    public StateMachine(PlayerController playerController)
    {
        PlayerController = playerController;
        
        JumpCountSetter = new JumpCountHandler(PlayerController.StatHandler.Data.MaxJumpCount);

        _groundCheck = playerController.GroundCheck;

        MovementState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
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
        CheckGround();
        _currentState.PhysicsUpdateState();
    }

    private void CheckGround()
    {
        if (PlayerController.Rigidbody.velocity.y < 0)
        {
            IsGrounded = _groundCheck.CheckIsGrounded();

            if (IsGrounded)
                JumpCountSetter.SetJumpCount(PlayerController.StatHandler.Data.MaxJumpCount);
        }


        Debug.Log(IsGrounded);
    }
}
