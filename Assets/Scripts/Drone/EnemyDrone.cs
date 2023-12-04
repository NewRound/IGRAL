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

    // �̵� - ������ ��ȯ��ġ ������ �������� �̵�
    // �Ѿ� �߻� - ���� �ֱ⸶�� �Ѿ��� �߻�

    public void ActiveDrone(float durationTime, Vector3 direction)
    {
        // Ȱ��ȭ
        _isActive = true;
        _durationTime = durationTime;
        _curDurationTime = 0.0f;
        gameObject.SetActive(true);
        _direction = direction;
        _attackDelay = Random.Range(0.5f, 1f);
    }

    private void InActiveDrone()
    {
        // ��Ȱ��ȭ
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

    // �Ѿ��� �߻��ϴ� �޼���
    private void OnFire()
    {
        GameObject projectile = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.BossDroneBullet);
        EnemyDroneBullet droneBullet = projectile.GetComponent<EnemyDroneBullet>();
        droneBullet.SetDirection(_direction);
        droneBullet.Activate();
        droneBullet.transform.position = transform.position;
    }
}
