using UnityEngine;

public class Grenade : ItemConsumable
{
    [SerializeField] private Grenade _grenade;
    private Collider _collider;
    private Transform _player;
    private Vector3 _direction;
    private float _damage;
    private float _speed;
    private float _duration;
    private float _bombTimer;

    public override void UseConsumable()
    {
        base.UseConsumable();
        _player = GameManager.Instance.PlayerTransform;

        Instantiate(_grenade, _player.position,Quaternion.identity);
    }

    private void Start()
    {
        _damage = 10f;
        _speed = 2f;
        _duration = 5f;
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        _bombTimer = 0f;

        // TODO 
        // �÷��̾� ���⿡ ���� ����
        // _direction = _player.position.y < 0 ? Vector3.right : Vector3.left;
    }

    private void Update()
    {
        _bombTimer += Time.deltaTime;
        if (_bombTimer >= _duration)
        { 
            OnBomb();
        }

        // TODO
        // �����̴°� �����ؾ���
        // _direction ���� _speed,
    }

    private void OnBomb()
    {
        // ������ ����Ʈ
        _collider.enabled = true;
        Destroy(gameObject, 1f);
        Debug.Log("������");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyStatHandler enemy = other.GetComponent<EnemyController>().StatHandler;
            if (enemy != null)
            {
                enemy.Damaged(_damage);
                Debug.Log("�� �浹");
            }
        }
    }
}
