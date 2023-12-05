using System.Collections.Generic;
using UnityEngine;
using GlobalEnums;
using System.Linq;

public class PlayerWeapon : Weapon
{
    [SerializeField] private float rayOffsetY = 0.8f;
    [SerializeField] private float rayOffsetYMod = 0.5f;
    [SerializeField] private float rayOffsetX = 0.2f;
    private PlayerSO _playerSO;
    private PlayerAnimationController _playerAnimationController;
    private Transform _modelTrans;
    private Transform _myTrans;
    private string _interactibleTag;
    private LayerMask _layer;
    private void Start()
    {
        targetTag = Tag.Enemy.ToString();
        _layer = 1 << LayerMask.NameToLayer(targetTag) | 1 << LayerMask.NameToLayer(Tag.Interactable.ToString());
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();

        InputController inputController = GetComponent<InputController>();
        _playerSO = inputController.StatHandler.Data;
        _modelTrans = inputController.StateMachine.ModelTrans;
        _myTrans = inputController.transform;

        _playerAnimationController.AttackAction += OnAttack;

    }

    private void OnDestroy()
    {
        _playerAnimationController.AttackAction -= OnAttack;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_myTrans == null)
            return;

        Vector3 offsetVec = _myTrans.position;
        rayOffsetX = _modelTrans.forward.x > 0 ? -rayOffsetX : rayOffsetX;
        offsetVec.x += rayOffsetX;
        offsetVec.y += rayOffsetY;
        Gizmos.DrawRay(offsetVec, _modelTrans.forward * _playerSO.AttackRange);
    }
#endif

    protected override void OnAttack()
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;
        Vector3 offsetVec = UpdateRayOffset();

        RaycastHit[] middle = Physics.RaycastAll(offsetVec, _modelTrans.forward, _playerSO.AttackRange, _layer);
        List<RaycastHit> hits = new List<RaycastHit>(middle);

        Vector3 topVec = offsetVec + new Vector3(0f, rayOffsetYMod, 0f);
        RaycastHit[] top = Physics.RaycastAll(topVec, _modelTrans.forward, _playerSO.AttackRange, _layer);
        hits.AddRange(top);

        Vector3 bottomVec = offsetVec + new Vector3(0f, -rayOffsetYMod, 0f);
        RaycastHit[] bottom = Physics.RaycastAll(bottomVec, _modelTrans.forward, _playerSO.AttackRange, _layer);
        hits.AddRange(bottom);

        hits.Distinct();

        foreach (RaycastHit hit in hits)
        {
            EnemyController enemyController = hit.collider.GetComponentInParent<EnemyController>();

            if (enemyController != null)
            {
                damageable = enemyController.StatHandler;
                EnemyStatHandler statHandler = enemyController.StatHandler;

                targetSO = statHandler.Data;
                damageable = statHandler;

                enemyController.StateMachine.Knockback(_modelTrans.forward, _playerSO.KnockbackPower);

                Attack(_playerSO, targetSO, damageable);
            }

            BossBehaviourTree bossBehaviourTree = hit.collider.GetComponent<BossBehaviourTree>();

            if (bossBehaviourTree != null)
            {
                EnemyStatHandler statHandler = bossBehaviourTree.StatHandler;
                targetSO = statHandler.Data;
                damageable = statHandler;

                Attack(_playerSO, targetSO, damageable);
            }

            IInteract interactable = hit.collider.GetComponent<IInteract>();
            if (interactable != null)
            {
                Attack(interactable);
            }
        }
    }

    private Vector3 UpdateRayOffset()
    {
        Vector3 offsetVec = _myTrans.position;
        rayOffsetX = _modelTrans.forward.x > 0 ? -rayOffsetX : rayOffsetX;
        offsetVec.x += rayOffsetX;
        offsetVec.y += rayOffsetY;
        return offsetVec;
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