using UnityEngine;

public class Bullet : BulletBase
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDuration;
    [SerializeField] private Transform modelTrans;
    [SerializeField] private float movePower = 10f;
    private Vector3 _direction;
    private float _curDuration;

    private void OnEnable()
    {
        _curDuration = 0f;
    }

    

    private void Update()
    {
        _curDuration += Time.deltaTime;

        if(_curDuration >= _maxDuration)
        {
            gameObject.SetActive(false);
        }
    }
    public void SetDirection(bool isRight)
    {
        _direction = isRight ? transform.right : -transform.right;
    }

    public void Move()
    {
        rigid.AddForce(_direction * movePower, ForceMode.Impulse);
    }

    public void Look(Vector3 direction)
    {
        transform.rotation = Quaternion.Euler(direction);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, _direction);
        modelTrans.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            PlayerStatHandler statHandler = playerController.StatHandler;

            if (statHandler != null)
                statHandler.Damaged(_damage);
            
            gameObject.SetActive(false);
        }

    }
}
