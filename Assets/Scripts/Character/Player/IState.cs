public interface IState
{
    void ChangeState();
    void Enter();
    void Exit();
    void UpdateState();
    void PhysicsUpdateState();
    void OnDead();
}