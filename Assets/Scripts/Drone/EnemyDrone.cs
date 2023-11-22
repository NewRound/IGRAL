using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    [Header("# Attack Info")]
    [SerializeField] private float _attackDelay;
    private float _attackTimer;

    [Header("# Move Info")]
    [SerializeField] private float _movementSpeed;

    private Rigidbody _rigid;

    private float _durationTime;
    private float _curDurationTime;
    private bool _isActive = false;

    // 이동 - 정해진 소환위치 정해진 방향으로 이동
    // 총알 발사 - 일정 주기마다 총알을 발사

    public void ActiveDrone(float durationTime)
    {
        // 활성화
        _isActive = true;
        _durationTime = durationTime;
        _curDurationTime = 0.0f;
        gameObject.SetActive(true);    
    }

    private void InActiveDrone()
    {
        // 비활성화
        _isActive = false;
        _durationTime = 0.0f;
        _curDurationTime = 0.0f;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_isActive)
            return;

        Move();

        _attackTimer += Time.deltaTime;

        if (_attackTimer > _attackDelay)
        {
            _attackTimer = 0;    
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
        Vector3 _direction = transform.position + Vector3.left;
        Vector3 lerpPos = Vector3.Lerp(transform.position, _direction, _movementSpeed * Time.deltaTime);

        _rigid.MovePosition(lerpPos);
    }

    // 총알을 발사하는 메서드
    private void OnFire()
    {
        GameObject projectile = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDroneBullet);
        DroneBullet droneBullet = projectile.GetComponent<DroneBullet>();      
        droneBullet.transform.position = transform.position;
    }
}
