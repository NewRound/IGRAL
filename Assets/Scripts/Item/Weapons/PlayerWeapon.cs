using UnityEditor.U2D.Aseprite;
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
        Vector3 offsetVec = _myTrans.position;
        offsetVec.y = 0.5f;
        Gizmos.DrawRay(offsetVec, _modelTrans.forward * 10f);
    }

    protected override void OnAttack()
    {
        HealthSO targetSO = null;
        IDamageable damageable = null;
        Vector3 offsetVec = _myTrans.position;
        offsetVec.y = 0.5f;


        RaycastHit[] hits = Physics.RaycastAll(offsetVec, _modelTrans.forward, 10f, 1 << LayerMask.NameToLayer(targetTag));
        //int combo = _playerAnimationController.AttackCombo;

        foreach (RaycastHit hit in hits)
        {
            EnemyStatHandler statHandler = hit.collider.GetComponentInParent<EnemyController>().StatHandler;

            targetSO = statHandler.Data;
            damageable = statHandler;

            Attack(_playerSO, targetSO, damageable);
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