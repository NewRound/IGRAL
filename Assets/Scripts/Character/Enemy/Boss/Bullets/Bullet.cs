using UnityEngine;

public class Bullet : Weapon
{
    [SerializeField] private float damage;
    [SerializeField] private float maxDuration;
    [SerializeField] private Transform modelTrans;
    [SerializeField] private float movePower = 10f;
    private float _curDuration;

    private Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _curDuration = 0f;
    }

    private void OnDisable()
    {
        _rigid.velocity = Vector3.zero;
    }


    private void Update()
    {
        _curDuration += Time.deltaTime;

        if(_curDuration >= maxDuration)
        {
            gameObject.SetActive(false);
        }
    }

    public void Move()
    {
        _rigid.AddForce(transform.forward * movePower, ForceMode.Impulse);
    }

    public void Look(Vector3 direction)
    {
        SetDirection(direction);
    }

    private void SetDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            PlayerStatHandler statHandler = playerController.StatHandler;

            if (statHandler != null)
                Attack(damage, statHandler.Data, statHandler);
            
            gameObject.SetActive(false);
        }

    }
}
