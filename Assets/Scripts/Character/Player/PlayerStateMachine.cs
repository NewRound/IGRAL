using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public InputController InputController { get; private set; }

    [field: Header("States")]
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    [field: Header("Roll")]
    public RollDataHandler RollDataHandler { get; private set; }

    private PlayerStatHandler _playerStatHandler;

    public PlayerMovementDataHandler MovementDataHandler;

    public PlayerStateMachine(InputController inputController)
    {
        InputController = inputController;

        _playerStatHandler = InputController.StatHandler;

        RollDataHandler = new RollDataHandler(
            _playerStatHandler.Data.RollingCoolTime,
            _playerStatHandler.Data.InvincibleTime);

        MovementDataHandler = new PlayerMovementDataHandler(
            InputController.MovementData,
            _playerStatHandler.Data.SpeedMin,
            _playerStatHandler.Data.SpeedMax,
            RollDataHandler,
            inputController.Rigidbody);

        JumpCountHandler = new JumpCountHandler(_playerStatHandler.Data.JumpingCountMax);
        GroundDataHandler = new GroundDataHandler(InputController.GroundData);

        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        RollState = new PlayerRollState(this);
    }

    public override void Init()
    {
        GroundDataHandler.Init(InputController.transform);
        ChangeState(MoveState);
    }

    public override void Update()
    {
        base.Update();
        RollDataHandler.CalculateCoolTime();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        CheckGround();
    }

    protected override void CheckGround()
    {
        if (InputController.Rigidbody.velocity.y < 0)
        {
            if (GroundDataHandler.IsGrounded)
            {
                JumpCountHandler.ResetJumpCount();
            }
            else
            {
                if (JumpCountHandler.JumpCount == _playerStatHandler.Data.JumpingCountMax)
                    JumpCountHandler.DecreaseJumpCount();
            }
        }
    }
}