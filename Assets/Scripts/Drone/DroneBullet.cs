using UnityEngine;

public enum BulletType
{
   playerDrone,
   EnmeyDrone
}

public class DroneBullet : Weapon
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxDuration;
    [SerializeField] private BulletType _bulletType;
    private float _curDuration;

    private Vector3 _direction;
    private Transform _target;

    private void OnEnable()
    { 
        _curDuration = 0f;
    }

    private void OnDisable()
    {
        _target = null;
    }

    private void Update()
    {
        Vector3 direction;

        switch (_bulletType)
        {
            case BulletType.playerDrone:
                if (_target != null)
                {
                    Vector3 targetPos = _target.position + new Vector3(0f, 1f, 0f);
                    direction = targetPos - transform.position;
                    transform.forward = direction.normalized;
                    transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
                }                
                break;

            case BulletType.EnmeyDrone:
                transform.forward = _direction;
                transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);
                break;
        }        

        _curDuration += Time.deltaTime;

        if (_curDuration >= _maxDuration)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void SetTarget(Transform target)
    {      
        _target = target;        
    }    

    private void OnTriggerEnter(Collider other)
    {
        switch(_bulletType)
        {
            case BulletType.playerDrone:
                EnemyController enemyController = other.GetComponent<EnemyController>();

                if (enemyController != null)
                {
                    EnemyStatHandler enemy = enemyController.StatHandler;
                    if (enemy != null)
                    {
                        enemy.Damaged(_attackDamage);
                    }
                    gameObject.SetActive(false);
                }
                break;

            case BulletType.EnmeyDrone:
                PlayerController playerController = other.GetComponent<PlayerController>();

                if(playerController != null)
                {
                    PlayerStatHandler player = playerController.StatHandler;
                    if( player != null)
                    {
                        Attack(_attackDamage, player.Data, player);
                    }
                    gameObject.SetActive(false) ;
                }
                break;
        }       
    }
}
