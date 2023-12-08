using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    [Header("# Attack Info")]
    private float _attackDelay;
    private float _attackTimer;

    [Header("# Move Info")]
    [SerializeField] private float _movementSpeed;
    
    private float _durationTime;
    private float _curDurationTime;
    private bool _isActive = false;

    private Vector3 _direction;

    // 이동 - 정해진 소환위치 정해진 방향으로 이동
    // 총알 발사 - 일정 주기마다 총알을 발사

    public void ActiveDrone(float durationTime, Vector3 direction)
    {
        // 활성화
        _isActive = true;
        _durationTime = durationTime;
        _curDurationTime = 0.0f;
        gameObject.SetActive(true);
        _direction = direction;
        _attackDelay = Random.Range(0.5f, 1f);
    }

    private void InActiveDrone()
    {
        // 비활성화
        _isActive = false;
        _durationTime = 0.0f;
        _curDurationTime = 0.0f;
        gameObject.SetActive(false);
        _direction = Vector3.zero;
        _attackTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (!_isActive)
            return;

        Move();

        _attackTimer += Time.deltaTime;

        if (_attackTimer > _attackDelay)
        {
            _attackTimer = 0f;    
            OnFire();           
        }

        _curDurationTime += Time.deltaTime;
        if (_curDurationTime >= _durationTime)
        {
            InActiveDrone();
        }
    }

    private void Move()
    {        
        transform.forward = _direction;
        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
    }

    // 총알을 발사하는 메서드
    private void OnFire()
    {
        GameObject projectile = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.BossDroneBullet);
        EnemyDroneBullet droneBullet = projectile.GetComponent<EnemyDroneBullet>();
        droneBullet.SetDirection(_direction);
        droneBullet.Activate();
        droneBullet.transform.position = transform.position;
    }
}
