using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : CharacterWeapon
{
    private EnemySO _enemySO;
    private EnemyAnimationController _enemyAnimationController;

    private void Start()
    {
        _enemyAnimationController = GetComponentInParent<EnemyAnimationController>();
        targetTag = GlobalEnums.Tag.Player.ToString();
        layer = 1 << LayerMask.NameToLayer(targetTag);

        EnemyController enemyController = GetComponentInParent<EnemyController>();
        _enemySO = enemyController.StatHandler.Data;
        modelTrans = enemyController.StateMachine.ModelTrans;
        myTrans = enemyController.transform;

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
        Vector3 offsetVec = UpdateRayOffset();

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
            bool isHit = Physics.Raycast(vector, modelTrans.forward, out hit, _enemySO.AttackRange, layer);
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