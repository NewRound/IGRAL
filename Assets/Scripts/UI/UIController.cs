using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Button _healing;
    [SerializeField] private Button _jump;
    [SerializeField] private Button _slide;
    [SerializeField] private Button _skill;
    [SerializeField] private Button _attack;
    [SerializeField] private Button _interaction;
    [SerializeField] private Button _pickup;
    [SerializeField] private Button _item;

    [Header("GameObject")]
    [SerializeField] private GameObject _attackObj;
    [SerializeField] private GameObject _interactionObj;
    [SerializeField] private GameObject _pickupObj;

    private Vector2 _direction = Vector2.zero;
    private Vector2 _temp = Vector2.zero;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GameManager.Instance.player.GetComponent<PlayerController>();

        _healing.onClick.AddListener(OnHealingButton);
        _jump.onClick.AddListener(OnJumpButton);
        _slide.onClick.AddListener(OnSlideButton);
        _skill.onClick.AddListener(OnSkillButton);
        _attack.onClick.AddListener(OnAttackButton);
        _interaction.onClick.AddListener(OnInteractionButton);
        _pickup.onClick.AddListener(OnPickupButton);
        _item.onClick.AddListener(OnItemButton);

    }

    private void Start()
    {
        SwitchingAttack();
    }

    private void FixedUpdate()
    {
        _temp = Vector2.right * variableJoystick.Horizontal;
        
        if(_direction != _temp)
        {
            _direction = _temp;
            _playerController.CallMoveAction(_direction);
        }
    }

    #region 버튼 클릭 이벤트
    private void OnHealingButton()
    {

    }

    private void OnJumpButton()
    {
        _playerController.CallJumpAction();
    }

    private void OnSlideButton()
    {
        _playerController.CallRollAction();
    }

    private void OnSkillButton()
    {

    }

    private void OnAttackButton()
    {
        _playerController.CallAttackAction();
    }

    private void OnInteractionButton()
    {

    }

    private void OnPickupButton()
    {

    }

    private void OnItemButton()
    {

    }
    #endregion 버튼 클릭 이벤트

    #region 버튼 스위칭
    public void SwitchingAttack()
    {
        _attackObj.SetActive(true);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(false);
    }

    public void SwitchingInteraction()
    {
        _attackObj.SetActive(false); 
        _interactionObj.SetActive(true);
        _pickupObj.SetActive(false);
    }

    public void SwitchingPickup()
    {
        _attackObj.SetActive(false);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(true); 
    }
    #endregion 버튼 스위칭
}
