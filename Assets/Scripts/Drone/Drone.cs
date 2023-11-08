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

    [SerializeField] private Transform _followTarget;
    
    public void ActiveDrone()
    {
        // 활성화
        gameObject.SetActive(true);
    }

    public void InActiveDrone()
    {
        // 비활성화
        gameObject.SetActive(false);
    }

    private void Update()
    {
        SetTransform();
        
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
                    Debug.Log("Target In Range");                    
                    OnFire(hit);
                    break;
                }

                else
                { 
                    Debug.Log("There is no Target");
                }
            }
        }
    }

    private void SetTransform()
    {
        transform.position = _followTarget.position;
        transform.rotation = _followTarget.rotation;
    }

    // 총알을 발사하는 메서드
    private void OnFire(RaycastHit hitInfo)
    {
        Debug.Log("OnFire");
        DroneProjectile projectile = ProjectilePool.Instance.GetProjectile();

        projectile.SetTarget(hitInfo.transform);
        projectile.transform.position = transform.position;        
    }    
}
