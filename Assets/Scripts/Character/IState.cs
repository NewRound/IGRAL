public interface IState
{
    void Enter();
    void Exit();
    void UpdateState();
    void PhysicsUpdateState();
    void OnDead();
}