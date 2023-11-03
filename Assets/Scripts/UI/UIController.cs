using UnityEngine;
using UnityEngine.UI;

public class UIController : CustomSingleton<UIController>
{
    [Header("Button")]
    [SerializeField] private VariableJoystick _moveJoystick;
    [SerializeField] private VariableJoystick _skillJoystick;
    [SerializeField] private Button _healing;
    [SerializeField] private Button _jump;
    [SerializeField] private Button _slide;
    [SerializeField] private Button _skill;
    [SerializeField] private Button _attack;
    [SerializeField] private Button _interaction;
    [SerializeField] private Button _pickup;
    [SerializeField] private Button _item;
    [SerializeField] private Button _talk;

    [Header("GameObject")]
    [SerializeField] private GameObject _moveJoystickObj;
    [SerializeField] private GameObject _skillJoystickObj;
    [SerializeField] private GameObject _attackObj;
    [SerializeField] private GameObject _interactionObj;
    [SerializeField] private GameObject _pickupObj;
    [SerializeField] private GameObject _talkObj;
    [SerializeField] private SkillUse[] _skillUse;

    private Vector2 _direction = Vector2.zero;
    private Vector2 _temp = Vector2.zero;
    private float _correctionValue;
    public bool isSkill = false;
    private bool _isMove = true;

    private InputController _inputController;
    private GameObject _interactiveObject;


    private void Awake()
    {
        _inputController = GameManager.Instance.player.GetComponent<InputController>();

        _healing.onClick.AddListener(OnHealingButton);
        _jump.onClick.AddListener(OnJumpButton);
        _slide.onClick.AddListener(OnSlideButton);
        _skill.onClick.AddListener(OnSkillButton);
        _attack.onClick.AddListener(OnAttackButton);
        _interaction.onClick.AddListener(OnInteractionButton);
        _pickup.onClick.AddListener(OnPickupButton);
        _item.onClick.AddListener(OnItemButton);
        _talk.onClick.AddListener(OnTalkButton);
    }

    private void Start()
    {
        SwitchingAttack();
        SkillManager.Instance.SetSkillUes(_skillUse);
    }

    private void FixedUpdate()
    {
        if (isSkill)
        {
            if(_isMove)
            {
                _moveJoystickObj.transform.localScale = Vector3.one * 0.5f;
                _skillJoystickObj.transform.localScale = Vector3.one * 2;

                foreach(SkillUse skillUse in _skillUse)
                {
                    skillUse.DisplaySkill();
                }

                _isMove = false;
            }
            
            _inputController.CallMoveAction(Vector2.zero);
            _temp = Vector2.zero;
            _direction = Vector2.zero;

            if (_skillJoystick.Horizontal == 0 || _skillJoystick.Vertical == 0)
            {
                isSkill = false;
            }
        }
        else
        {
            if (!_isMove)
            {
                _moveJoystickObj.transform.localScale = Vector3.one;
                _skillJoystickObj.transform.localScale = Vector3.one;
                foreach (SkillUse skillUse in _skillUse)
                {
                    skillUse.NoDisplaySkill();
                }
                _skillJoystickObj.SetActive(false);
                _skillJoystickObj.SetActive(true);
                _isMove = true;
            }
            _correctionValue = _moveJoystick.Horizontal;
            if (_correctionValue != 0)
            {
                _correctionValue = _correctionValue < 0 ? -1 : 1;
            }
            _temp = Vector2.right * _correctionValue;

            if (_direction != _temp)
            {
                _direction = _temp;
                _inputController.CallMoveAction(_direction);
            }

            if (_skillJoystick.Horizontal != 0 || _skillJoystick.Vertical != 0)
            {
                isSkill = true;
            }
        }
    }

    #region 상호작용 게임오브젝트 관리
    public void SetInteractiveObject(GameObject go)
    {
        _interactiveObject = go;
    }

    public void DelInteractiveObject()
    {
        _interactiveObject = null;
    }
    #endregion

    #region 버튼 클릭 이벤트
    private void OnHealingButton()
    {

    }

    private void OnJumpButton()
    {
        _inputController.CallJumpAction();
    }

    private void OnSlideButton()
    {
        _inputController.CallRollAction();
    }

    private void OnSkillButton()
    {

    }

    private void OnAttackButton()
    {
        _inputController.CallAttackAction();
    }

    private void OnInteractionButton()
    {
        _interactiveObject.GetComponent<InteractiveObject>().Use();
    }

    private void OnPickupButton()
    {
        ItemManager.Instance.pickupItem.GetComponent<PickupObject>().Pickup();
    }

    private void OnItemButton()
    {

    }

    private void OnTalkButton()
    {

    }
    #endregion 버튼 클릭 이벤트

    #region 버튼 스위칭
    public void SwitchingAttack()
    {
        _attackObj.SetActive(true);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(false);
        _talkObj.SetActive(false);
    }

    public void SwitchingInteraction()
    {
        _attackObj.SetActive(false); 
        _interactionObj.SetActive(true);
        _pickupObj.SetActive(false);
        _talkObj.SetActive(false);
    }

    public void SwitchingPickup()
    {
        _attackObj.SetActive(false);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(true);
        _talkObj.SetActive(false);
    }

    public void SwitchingTalk()
    {
        _attackObj.SetActive(false);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(false); 
        _talkObj.SetActive(true);
    }
    #endregion 버튼 스위칭
}
