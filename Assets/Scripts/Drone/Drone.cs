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
    private Vector3 xyOffset;
    
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
        // SetPosition();
        
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

    //private void SetPosition()
    //{
    //    if(_followTarget.rotation.y < 0)
    //    {
    //        xyOffset = new Vector3(1,2,0);
    //    }
    //    else if (_followTarget.rotation.y > 0)
    //    {
    //        xyOffset = new Vector3(-1, 2, 0);
    //    }

    //    transform.SetPositionAndRotation(_followTarget.position + xyOffset, _followTarget.rotation);
        
    //}

    // 총알을 발사하는 메서드
    private void OnFire(RaycastHit hitInfo)
    {
        Debug.Log("OnFire");
        DroneProjectile projectile = ProjectilePool.Instance.GetProjectile();

        projectile.SetTarget(hitInfo.transform);
        projectile.transform.position = transform.position;        
    }    
}
