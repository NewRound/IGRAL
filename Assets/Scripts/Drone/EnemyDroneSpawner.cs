using UnityEngine;

public class EnemyDroneSpawner
{
    private float _durationTime;

    private Vector3 _direction;

    private Transform[] _spawnTrans;

    private float _centerXPos;

    private float _droneHeight;

    private Transform _playerTrans;

    public EnemyDroneSpawner(Transform[] spawnTrans, float durationTime, float droneHeight)
    {
        _spawnTrans = new Transform[2];
        _spawnTrans[0] = spawnTrans[0];
        _spawnTrans[1] = spawnTrans[spawnTrans.Length - 1];
        _centerXPos = (spawnTrans[0].position.x + _spawnTrans[1].position.x) * GlobalValues.HALF;
        _durationTime = durationTime;
        _droneHeight = droneHeight;
    }

    public void SpawnDrone()
    {
        GameObject _enemyDrone = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDrone);
        
        _direction = GetDirection();
         
        Vector3 spawnPos = _direction == Vector3.left ? _spawnTrans[0].position : _spawnTrans[1].position;
        spawnPos.y += _droneHeight;
        _enemyDrone.transform.position = spawnPos;

        EnemyDrone _drone = _enemyDrone.GetComponent<EnemyDrone>();
        _drone.ActiveDrone(_durationTime, _direction);
    }

    private Vector3 GetDirection()
    {
        if (_playerTrans == null)
            _playerTrans = GameManager.Instance.PlayerTransform;

        if (_centerXPos <= _playerTrans.position.x) return Vector3.left;
        else return Vector3.right;
    }
}
