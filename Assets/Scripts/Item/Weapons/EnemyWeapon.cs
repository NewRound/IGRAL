using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    [SerializeField] private float rayOffsetY = 0.8f;
    [SerializeField] private float rayOffsetYMod = 0.5f;
    [SerializeField] private float rayOffsetX = 0.2f;
    private LayerMask _layer;

    private EnemySO _enemySO;
    private EnemyAnimationController _enemyAnimationController;
    private Transform _modelTrans;
    private Transform _myTrans;

    private void Start()
    {
        _enemyAnimationController = GetComponentInParent<EnemyAnimationController>();
        targetTag = GlobalEnums.Tag.Player.ToString();
        _layer = 1 << LayerMask.NameToLayer(targetTag);

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

        Vector3 topVec = offsetVec + new Vector3(0f, rayOffsetYMod, 0f);
        Vector3 bottomVec = offsetVec + new Vector3(0f, -rayOffsetYMod, 0f);

        List<Vector3> originVectors = new List<Vector3>() 
        {
            offsetVec,
            topVec,
            bottomVec,
        };

        RaycastHit hit;
        foreach (var vector in originVectors)
        {
            bool isHit = Physics.Raycast(vector, _modelTrans.forward, out hit, _enemySO.AttackRange, _layer);
            if (isHit)
            {
                PlayerStatHandler statHandler = hit.collider.GetComponentInParent<PlayerController>().StatHandler;

                targetSO = statHandler.Data;
                damageable = statHandler;
                Attack(_enemySO, targetSO, damageable);
                return;
            }
        }
    }
}