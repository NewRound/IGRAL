using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    [SerializeField] private float _attackDamage;

    private Vector3 _myPos;
    private Vector3 _targetPos;

    private Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }    

    private void OnEnable()
    {
        _myPos = transform.position;
    }

    private void Update()
    {
        // Ÿ�� ��ġ�� �̵��ϱ�
        // rigidbody moveposition 
    }

    private void OnTriggerEnter(Collider other)
    {
        // ������ �ε����� ������ �ְ� ��Ȱ��ȭ
        if (other.CompareTag("Enemy")) 
        {
            other.GetComponent<EnemyStatHandler>().Damaged(_attackDamage);
            gameObject.SetActive(false);
        }
    }
}
