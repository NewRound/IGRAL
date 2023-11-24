using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{
    [Header("# Attack Info")]
    [SerializeField] private float _attackDelay;
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
    }

    private void InActiveDrone()
    {
        // ��Ȱ��ȭ
        _isActive = false;
        _durationTime = 0.0f;
        _curDurationTime = 0.0f;
        gameObject.SetActive(false);
        _direction = Vector3.zero;
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
        transform.forward = _direction;
        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
    }

    // �Ѿ��� �߻��ϴ� �޼���
    private void OnFire()
    {
        GameObject projectile = ObjectPoolingManager.Instance.GetGameObject(ObjectPoolType.EnemyDroneBullet);
        DroneBullet droneBullet = projectile.GetComponent<DroneBullet>();      
        droneBullet.transform.position = transform.position;
    }
}