using UnityEngine;

public class EnemyDroneSpawner
{
    private float _durationTime;

    private Vector3 _direction;

    private Transform _spawnTrans;

    public EnemyDroneSpawner(Transform spawnTrans, float durationTime)
    {
        _spawnTrans = spawnTrans;
        _durationTime = durationTime;
    }

    public void SpawnDrone()
    {
        _direction = GetDirection();
        GameObject _enemyDrone = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDrone);
        Vector3 spawnPos = _spawnTrans.position;
        
        spawnPos.y += 5f;
        _enemyDrone.transform.position = spawnPos;

        EnemyDrone _drone = _enemyDrone.GetComponent<EnemyDrone>();
        _drone.ActiveDrone(_durationTime, _direction);
    }

    private Vector3 GetDirection()
    {
        Transform player = GameManager.Instance.PlayerTransform;
        if (_spawnTrans.position.x - player.position.x >= 0) return Vector3.left;
        else return Vector3.right;
    }
}
