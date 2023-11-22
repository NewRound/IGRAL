using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    private float _attackDamage;
    private float _movementSpeed;
    private float _maxDuration;
    private float _curDuration;
    private Transform _target;

    private void OnEnable()
    {
        _target = GameManager.Instance.PlayerTransform;
    }

    private void Start ()
    {
        _target = GameManager.Instance.PlayerTransform;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
        _curDuration += Time.deltaTime;

        if (_curDuration >= _maxDuration)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetEnemyBullet(EnemySO enemySO)
    { 
        Vector3 targetPos = _target.position + new Vector3(0f, 1f, 0f);
        Vector3 direction = targetPos - transform.position;
        transform.forward = direction.normalized;

        _attackDamage = enemySO.Attack;
        _movementSpeed = enemySO.ProjectileSpeed;
        _maxDuration = enemySO.ProjectileDuration;
        _curDuration = 0f;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (_targetLayer.value == (_targetLayer.value | (1 << other.gameObject.layer)))
        {
            PlayerStatHandler player = other.GetComponent<PlayerController>().StatHandler;
            if(player != null)
            {
                player.Damaged(_attackDamage);
            }            
            gameObject.SetActive(false);
        }
    }
}
