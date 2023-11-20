using UnityEngine;

public class EnemyWeapon : Weapon
{
    //[field: SerializeField] private AttackData attackData;
    private EnemySO _enemySO;
    private EnemyAnimationController _enemyAnimationController;
    private Transform _modelTrans;
    private Transform _myTrans;

    private void Start()
    {
        _enemyAnimationController = GetComponentInParent<EnemyAnimationController>();

        targetTag = GlobalEnums.Tag.Player.ToString();

        EnemyController enemyController = GetComponentInParent<EnemyController>();
        _enemySO = enemyController.StatHandler.Data;
        _modelTrans = enemyController.StateMachine.ModelTrans;
        _myTrans = enemyController.transform;

        _enemyAnimationController.AttackAction += OnAttack;
    }

    private void OnDestroy()
    {
        _enemyAnimationController.AttackAction -= OnAttack;
    }

    public void Init(EnemySO enemySO)
    {
        _enemySO = enemySO;
        targetTag = GlobalEnums.Tag.Player.ToString();
    }

    public void UpdateStat(EnemySO enemySO)
    {
        _enemySO = enemySO;
    }

    protected override void OnAttack()
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;
        Vector3 offsetVec = _myTrans.position;
        offsetVec.y += 0.5f;

        Ray ray = new Ray(offsetVec, _modelTrans.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _enemySO.AttackRange, 1 << LayerMask.NameToLayer(targetTag)))
        {
            PlayerStatHandler statHandler = hit.collider.GetComponentInParent<PlayerController>().StatHandler;

            targetSO = statHandler.Data;
            damageable = statHandler;
            Attack(_enemySO, targetSO, damageable);
        }
    }
}