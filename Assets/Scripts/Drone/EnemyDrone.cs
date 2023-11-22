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

    // �̵� - ������ ��ȯ��ġ ������ �������� �̵�
    // �Ѿ� �߻� - ���� �ֱ⸶�� �Ѿ��� �߻�

    public void ActiveDrone(float durationTime)
    {
        // Ȱ��ȭ
        _isActive = true;
        _durationTime = durationTime;
        _curDurationTime = 0.0f;
        gameObject.SetActive(true);    
    }

    private void InActiveDrone()
    {
        // ��Ȱ��ȭ
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

    // �Ѿ��� �߻��ϴ� �޼���
    private void OnFire()
    {
        GameObject projectile = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDroneBullet);
        DroneBullet droneBullet = projectile.GetComponent<DroneBullet>();      
        droneBullet.transform.position = transform.position;
    }
}
