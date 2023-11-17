using UnityEditor;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public InputController InputController { get; private set; }

    [field: Header("States")]
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerComboAttackState ComboAttackState { get; private set; }
    public PlayerDieState DieState { get; private set; }


    [field: Header("Roll")]
    public RollDataHandler RollDataHandler { get; private set; }

    [field: Header("Jump")]
    public JumpCountHandler JumpCountHandler { get; private set; }

    [field: Header("Ground")]
    public GroundDataHandler GroundDataHandler { get; private set; }

    [Header("Stat")]
    private PlayerStatHandler _playerStatHandler;

    public PlayerStateMachine(InputController inputController)
    {
        InputController = inputController;

        _playerStatHandler = InputController.StatHandler;

        Rigid = InputController.Rigidbody;
        ModelTrans = InputController.MovementData.ModelTrans;

        RollDataHandler = new RollDataHandler(
            _playerStatHandler.Data.RollingCoolTime,
            _playerStatHandler.Data.InvincibleTime);

        SpeedCalculator = new SpeedCalculator(InputController.MovementData.AcceleratingTime);
        RotationCalculator = new RotationCalculator(InputController.MovementData.RotationSpeed);

        JumpCountHandler = new JumpCountHandler(_playerStatHandler.Data.JumpingCountMax);
        GroundDataHandler = new GroundDataHandler(InputController.GroundData);

        speedMin = InputController.StatHandler.Data.SpeedMin;
        speedMax = InputController.StatHandler.Data.SpeedMax;

        SetPreDirection(Vector3.right);
        InputController.AttackAction += OnAttackInput;

        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        RollState = new PlayerRollState(this);
        ComboAttackState = new PlayerComboAttackState(this);
        DieState = new PlayerDieState(this);
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
        GroundDataHandler.CheckIsGrounded();
        CheckGround();
    }

    private void CheckGround()
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

    public override void Look()
    {
        if (RollDataHandler.IsRolling)
            return;

        if (Direction.x == 0)
        {
            LookPreDirectionRightAway();
            return;
        }

        base.Look();
    }

    public override Quaternion GetRotation()
    {
        return RotationCalculator.CalculateRotation(ModelTrans.rotation, PreDirection);
    }

    public void OnAttackInput()
    {
        if (CurrentState != ComboAttackState)
            ChangeState(ComboAttackState);
    }

    public override void Move()
    {
        base.Move();
    }

    public void Ondead()
    {
        InputController.AttackAction -= OnAttackInput;
        ChangeState(DieState);
    }
}
