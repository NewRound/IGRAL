using UnityEngine;

public class Bullet : BulletBase
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxDuration;
    private float _curDuration;

    private void OnEnable()
    {
        _curDuration = 0f;
    }

    public void Move(bool isRight)
    {
        Vector3 direction = isRight ? transform.right : -transform.right;
        rigid.AddForce(direction * 10f, ForceMode.Impulse);
    }

    private void Update()
    {
        _curDuration += Time.deltaTime;

        if(_curDuration >= _maxDuration)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            PlayerStatHandler statHandler = playerController.StatHandler;

            if (statHandler != null)
            {
                statHandler.Damaged(_damage);
            }
            gameObject.SetActive(false);
        }
    }
}
