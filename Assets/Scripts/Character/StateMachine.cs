public class StateMachine
{
    private IState _currentState;

    public PlayerController PlayerController { get; private set; }

    public PlayerMoveState MovementState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    
    public StateMachine(PlayerController playerController)
    {
        PlayerController = playerController;

        MovementState = new PlayerMoveState(this);
    }

    public void Init()
    {
        ChangeState(MovementState);
        ChangeState(JumpState);
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
