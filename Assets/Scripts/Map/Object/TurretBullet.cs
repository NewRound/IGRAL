using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] private float _attackDamage;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxDuration;
    private float _curDuration;
    private Vector3 _direction = Vector3.zero;

    private void OnEnable()
    {
        _curDuration = 0f;
    }

    private void OnDisable()
    {
        _direction = Vector3.zero;
    }

    private void Update()
    {
        if (_curDuration >= _maxDuration)
        {
            gameObject.SetActive(false);
        }

        if (_direction != Vector3.zero)
        {
            transform.forward = _direction.normalized;
            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
        }
        _curDuration += Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        InputController inputController = other.GetComponent<InputController>();

        if (inputController != null)
        {
            PlayerAppearanceController playerAppearanceController = other.GetComponent<PlayerAppearanceController>();

            if(playerAppearanceController.mutantType != MutantType.Sheld)
            {
                PlayerStatHandler player = other.GetComponent<PlayerController>().StatHandler;
                if (player != null)
                {
                    player.Damaged(_attackDamage);
                }
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if(_targetLayer == (1 << other.gameObject.layer))
        {
            gameObject.SetActive(false);
        }
    }
}
