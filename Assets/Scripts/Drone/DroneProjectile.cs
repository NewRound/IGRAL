using UnityEngine;

public class DroneProjectile : MonoBehaviour
{
    private float _attackDamage;
    private Vector3 _myPos;
    private Vector3 _targetPos;

    private Rigidbody _rigid;

    public DroneProjectile(Vector3 tagetPos, float attackDamage)
    {
        _targetPos = tagetPos;
        this._attackDamage = attackDamage;
    }

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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        {
            gameObject.SetActive(false);
        }
    }
}
