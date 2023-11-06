using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxDuration;
    private float _curDuration;

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
        if(_target != null)
        {
            Vector3 direction = _target.position - transform.position;
            transform.forward = direction.normalized;
            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
        }

        _curDuration += Time.deltaTime;

        if (_curDuration >= _maxDuration)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;        
    }    

    private void OnTriggerEnter(Collider other)
    {
        // ������ �ε����� ������ �ְ� ��Ȱ��ȭ
        if (other.CompareTag("Enemy")) 
        {
            EnemyStatHandler enemy = other.GetComponent<EnemyStatHandler>();
            if(enemy != null)
            {
                enemy.Damaged(_attackDamage);
            }            
            gameObject.SetActive(false);
        }
    }
}
