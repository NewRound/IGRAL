using UnityEngine;

public class EnemyDroneSpawner : MonoBehaviour
{
    [SerializeField] private float durationTime;

    public void SpawnDrone()
    {
        GameObject _enemyDrone = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDrone);
        _enemyDrone.transform.position = transform.position;

        EnemyDrone _drone = _enemyDrone.GetComponent<EnemyDrone>();
        _drone.ActiveDrone(durationTime);
    }
}
