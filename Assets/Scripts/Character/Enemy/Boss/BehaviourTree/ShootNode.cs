using GlobalEnums;
using UnityEngine;

public class ShootNode : Node
{
    private Bullet _bullet;
    private Transform _target;
    private Transform _spawnPoint;
    private int _bulletAmount;


    public ShootNode(Bullet bullet, Transform spawnPoint, Transform target, int amount)
    {
        _bullet = bullet;
        _spawnPoint = spawnPoint;
        _target = target;
        _bulletAmount = amount;
    }

    public override NodeState Evaluate()
    {
        // TEST
        float angle = 15;
        float modAngle = 0;
        Vector3 direction = _target.position - _spawnPoint.position;

        bool isRight = direction.x > 0;

        for (int i = 0; i < _bulletAmount; i++)
        {
            int halfIndex = i / 2;
            if (_bulletAmount % 2 != 0 && i == 0)
                modAngle = 0;
            else
                modAngle = i % 2 == 0 ? -angle * (halfIndex + 1) : angle * (halfIndex + 1);

            Vector3 afterDirection = Quaternion.Euler(direction.normalized) * new Vector3(0, 0, modAngle);

            Bullet bullet = Object.Instantiate(_bullet, _spawnPoint.position, Quaternion.Euler(afterDirection));            
            bullet.Move(isRight);
        }

        state = NodeState.Success;
        return state;
    }
}
