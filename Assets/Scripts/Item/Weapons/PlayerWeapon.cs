using UnityEngine;

public class PlayerWeapon : Weapon
{
    [field: SerializeField] private AttackData attackData;
    private PlayerSO _playerSO;
    private PlayerAnimationController _playerAnimationController;
    private Transform _modelTrans;
    private Transform _myTrans;

    private void Start()
    {
        targetTag = Tag.Enemy.ToString();
        // 임시
        _playerAnimationController = GetComponentInParent<PlayerAnimationController>();

        InputController inputController = GetComponentInParent<InputController>();
        _playerSO = inputController.StatHandler.Data;
        _modelTrans = inputController.StateMachine.ModelTrans;
        _myTrans = inputController.transform;

        _playerAnimationController.AttackAction += OnAttack;

    }

    private void OnDestroy()
    {
        _playerAnimationController.AttackAction -= OnAttack;
    }

    private void OnDrawGizmos()
    {
        if (_myTrans == null)
            return;

        Vector3 offsetVec = _myTrans.position;
        offsetVec.y += 0.5f;
        Gizmos.DrawRay(offsetVec, _modelTrans.forward * _playerSO.AttackRange);
    }

    protected override void OnAttack()
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;
        Vector3 offsetVec = _myTrans.position;
        offsetVec.y += 0.5f;

        RaycastHit[] hits = Physics.RaycastAll(offsetVec, _modelTrans.forward, _playerSO.AttackRange, 1 << LayerMask.NameToLayer(targetTag));

        foreach (RaycastHit hit in hits)
        {
            damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                EnemyController enemyController = hit.collider.GetComponentInParent<EnemyController>();
                EnemyStatHandler statHandler = enemyController.StatHandler;

                targetSO = statHandler.Data;
                damageable = statHandler;

                enemyController.StateMachine.Knockback(_modelTrans.forward, _playerSO.KnockbackPower);

                Attack(_playerSO, targetSO, damageable);
                return;
            }

            IInteract interactable = hit.collider.GetComponent<IInteract>();
            if (interactable != null)
            {
                Attack(interactable);
            }
        }
    }

    public void Init(PlayerSO playerSO)
    {
        _playerSO = playerSO;
    }

    public void UpdateStat(PlayerSO playerSO)
    {
        _playerSO = playerSO;
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }

    
}