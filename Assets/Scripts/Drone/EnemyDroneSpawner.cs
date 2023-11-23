using UnityEngine;

public class EnemyDroneSpawner : MonoBehaviour
{
    [SerializeField] private float _durationTime;

    private Vector3 _direction;

    public void SpawnDrone()
    {
        _direction = GetDirection();
        GameObject _enemyDrone = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDrone);
        _enemyDrone.transform.position = transform.position;

        EnemyDrone _drone = _enemyDrone.GetComponent<EnemyDrone>();
        _drone.ActiveDrone(_durationTime, _direction);
    }

    private Vector3 GetDirection()
    {
        Transform player = GameManager.Instance.PlayerTransform;
        if (transform.position.x - player.position.x >= 0) return Vector3.left;
        else return Vector3.right;
    }
}
