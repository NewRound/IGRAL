using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public PlayerController PlayerController { get; private set; }

    [field: Header("States")]
    public PlayerMoveState MovementState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    [field: Header("Jump")]
    public JumpCountHandler JumpCountSetter { get; private set; }

    [Header("Ground")]
    private GroundCheck _groundCheck;
    public bool IsGrounded { get; private set; }

    [field: Header("Roll")]
    public RollDataHandler RollDataHandler { get; private set; }

    [field: Header("Move")]
    public MovementDataHandler MovementDataHandler { get; private set; }

    private PlayerStatHandler _playerStatHandler;

    public StateMachine(PlayerController playerController)
    {
        PlayerController = playerController;

        _playerStatHandler = PlayerController.StatHandler;

        JumpCountSetter = new JumpCountHandler(_playerStatHandler.Data.JumpingCountMax);

        RollDataHandler = new RollDataHandler(
            _playerStatHandler.Data.RollingCoolTime,
            _playerStatHandler.Data.InvincibleTime);

        MovementDataHandler = new MovementDataHandler(
            PlayerController.MovementData,
            _playerStatHandler, 
            RollDataHandler, 
            playerController.Rigidbody);

        _groundCheck = playerController.GroundCheck;

        MovementState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        RollState = new PlayerRollState(this);
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
        RollDataHandler.CalculateCoolTime();
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
            {
                JumpCountSetter.SetJumpCount(_playerStatHandler.Data.JumpingCountMax);
            }
            else
            {
                if (JumpCountSetter.JumpCount == _playerStatHandler.Data.JumpingCountMax)
                    JumpCountSetter.DecreaseJumpCount();
            }
        }
    }
}
