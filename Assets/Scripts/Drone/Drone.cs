using UnityEngine;

public class Drone : MonoBehaviour
{
    [Header("# Attack Info")]    
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;
    private float _attackTimer;

    [Header("# RayCast")]
    [SerializeField] private float _rayAngle;
    [SerializeField] private int _rayCount;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("# Move Info")]    
    [SerializeField] private float _movementSpeed;

    private InputController _player;
    private Transform _followTarget;
    private Rigidbody _rigid;

    private float _durationTime;
    private float _time;
    private bool _isActive = false;

    public void ActiveDrone(float durationTime)
    {
        _followTarget = _player.DronePos;
        // Ȱ��ȭ
        gameObject.SetActive(true);
        _durationTime = durationTime;
        gameObject.transform.position = _followTarget.position;
        _time = 0.0f;
        _isActive = true;
    }

    public void InActiveDrone()
    {
        // ��Ȱ��ȭ
        _isActive = false;
        _durationTime = 0.0f;
        _time = 0.0f;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _player = GameManager.Instance.PlayerInputController;

    }

    private void FixedUpdate()
    {
        if (!_isActive)
            return;

        MoveToFollowTarget();
        
        _attackTimer += Time.deltaTime;

        if (_attackTimer > _attackDelay)
        {
            _attackTimer = 0;

            for (int i = 0; i <= _rayCount; i++)
            {
                float angle = (float)i / _rayCount * _rayAngle - _rayAngle / 2.0f;
                Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * transform.forward;

                Debug.DrawRay(transform.position, rayDirection * _attackRange, Color.red);

                RaycastHit hit;                
                if (Physics.Raycast(transform.position, rayDirection, out hit, _attackRange, _enemyLayer))
                {               
                    OnFire(hit);
                    break;
                }
            }
        }

        _time += Time.deltaTime;
        if (_time >= _durationTime)
        {
            InActiveDrone();
        }
    }

    private void MoveToFollowTarget()
    {       
        Vector3 _followTargetPos = _followTarget.position;

        // ��ġ ���������� ������ ���߱�
        if ((_followTargetPos - transform.position).sqrMagnitude < 0.1f * 0.1f) return;

        // ���� ���� ����
        transform.forward = _followTarget.forward;

        // �ε巯�� ������ �����ϱ�
        Vector3 lerpPos = Vector3.Lerp(transform.position, _followTargetPos, _movementSpeed * Time.deltaTime);

        _rigid.MovePosition(lerpPos);
    }

    // �Ѿ��� �߻��ϴ� �޼���
    private void OnFire(RaycastHit hitInfo)
    {
        DroneBullet projectile = ProjectilePool.Instance.GetProjectile();
        // hitInfo.collider.bounds.center
        projectile.SetTarget(hitInfo.transform);
        projectile.transform.position = transform.position;        
    }    
}
