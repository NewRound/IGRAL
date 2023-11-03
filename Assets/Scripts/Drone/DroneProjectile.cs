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
        // 타겟 위치로 이동하기
        // rigidbody moveposition 
    }

    private void OnTriggerEnter(Collider other)
    {
        // 적에게 부딪히면 데미지 주고 비활성화
        if (other.CompareTag("Enemy")) 
        {
            other.GetComponent<EnemyStatHandler>().Damaged(_attackDamage);
            gameObject.SetActive(false);
        }
    }
}
