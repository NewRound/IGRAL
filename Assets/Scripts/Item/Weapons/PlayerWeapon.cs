using System.Collections.Generic;
using UnityEngine;
using GlobalEnums;
using System.Linq;
using System.Collections;

public class PlayerWeapon : CharacterWeapon
{
    private PlayerSO _playerSO;
    private PlayerAnimationController _playerAnimationController;

    private void Start()
    {
        targetTag = Tag.Enemy.ToString();
        layer = 1 << LayerMask.NameToLayer(targetTag) | 1 << LayerMask.NameToLayer(Tag.Interactable.ToString());
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();

        InputController inputController = GetComponent<InputController>();
        _playerSO = inputController.StatHandler.Data;
        modelTrans = inputController.StateMachine.ModelTrans;
        myTrans = inputController.transform;

        _playerAnimationController.AttackAction += OnAttack;

    }

    private void OnDestroy()
    {
        _playerAnimationController.AttackAction -= OnAttack;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (myTrans == null)
            return;

        Vector3 offsetVec = UpdateRayOffset();
        Gizmos.DrawRay(offsetVec, modelTrans.forward * _playerSO.AttackRange);

        Vector3 topVec = offsetVec + new Vector3(0f, rayOffsetYMod, 0f);
        Gizmos.DrawRay(topVec, modelTrans.forward * _playerSO.AttackRange);

        Vector3 bottomVec = offsetVec + new Vector3(0f, -rayOffsetYMod, 0f);
        Gizmos.DrawRay(bottomVec, modelTrans.forward * _playerSO.AttackRange);
    }
#endif

    protected override void OnAttack()
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;
        Vector3 offsetVec = UpdateRayOffset();

        HashSet<Collider> newHits = new HashSet<Collider>();

        RaycastHit[] middle = Physics.RaycastAll(offsetVec, modelTrans.forward, _playerSO.AttackRange, layer);
        List<RaycastHit> hits = new List<RaycastHit>(middle);

        Vector3 topVec = offsetVec + new Vector3(0f, rayOffsetYMod, 0f);
        RaycastHit[] top = Physics.RaycastAll(topVec, modelTrans.forward, _playerSO.AttackRange, layer);
        hits.AddRange(top);

        Vector3 bottomVec = offsetVec + new Vector3(0f, -rayOffsetYMod, 0f);
        RaycastHit[] bottom = Physics.RaycastAll(bottomVec, modelTrans.forward, _playerSO.AttackRange, layer);
        hits.AddRange(bottom);

        AddNewRaycastHit(newHits, hits);

        foreach (Collider hit in newHits)
        {
            EnemyController enemyController = hit.GetComponentInParent<EnemyController>();

            if (enemyController != null)
            {
                damageable = enemyController.StatHandler;
                EnemyStatHandler statHandler = enemyController.StatHandler;

                targetSO = statHandler.Data;
                damageable = statHandler;

                enemyController.StateMachine.Knockback(modelTrans.forward, _playerSO.KnockbackPower);

                Attack(_playerSO, targetSO, damageable);
            }

            BossBehaviorTree bossBehaviourTree = hit.GetComponent<BossBehaviorTree>();

            if (bossBehaviourTree != null)
            {
                EnemyStatHandler statHandler = bossBehaviourTree.StatHandler;
                targetSO = statHandler.Data;
                damageable = statHandler;

                Attack(_playerSO, targetSO, damageable);
            }

            IInteract interactable = hit.GetComponent<IInteract>();
            if (interactable != null)
            {
                Attack(interactable);
            }
        }
    }

    private void AddNewRaycastHit(HashSet<Collider> newHits, List<RaycastHit> hits)
    {
        foreach (RaycastHit hit in hits)
        {
            Collider collider = hit.collider;
            if (!newHits.Contains(collider))
                newHits.Add(collider);
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
}