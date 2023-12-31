using System;
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

    [Header("Image")]
    [SerializeField] private Image _emptied;
    [SerializeField] private Image _consumableItem;

    [Header("GameObject")]
    [SerializeField] private GameObject _moveJoystickObj;
    [SerializeField] private GameObject _skillJoystickObj;
    [SerializeField] private GameObject _attackObj;
    [SerializeField] private GameObject _interactionObj;
    [SerializeField] private GameObject _pickupObj;
    [SerializeField] private GameObject _talkObj;
    [SerializeField] private SkillUse[] _skillUse;
    [SerializeField] private GameObject[] _webGL;
    [SerializeField] private GameObject[] _KeyInfo;

    private Vector2 _direction = Vector2.zero;
    private Vector2 _temp = Vector2.zero;
    private float _correctionValue;
    public bool isSkill = false;
    private bool _isMove = true;
    private int _skillIndex = -1;

    private InputController _inputController;
    private GameObject _interactiveObject;

    private void InputControllerSet()
    {
        _inputController = GameManager.Instance.PlayerInputController;
        SwitchingAttack();
    }

    private void Awake()
    {
#if UNITY_ANDROID
        _jump.onClick.AddListener(OnJumpButton);
        _slide.onClick.AddListener(OnSlideButton);
        _attack.onClick.AddListener(OnAttackButton);
        _interaction.onClick.AddListener(OnInteractionButton);
        _pickup.onClick.AddListener(OnPickupButton);
        _item.onClick.AddListener(OnItemButton);
        _talk.onClick.AddListener(OnTalkButton);

#endif
        //_healing.onClick.AddListener(OnHealingButton);
        //_skill.onClick.AddListener(OnSkillButton);
    }

    private void Start()
    {
        SkillManager.Instance.SetSkillUes(_skillUse);
#if UNITY_ANDROID
        SwitchingAttack();
        InputControllerSet();
        GameManager.Instance.SceneLoad += InputControllerSet;

        foreach (GameObject go in _KeyInfo)
        {
            go.SetActive(false);
        }            
#endif
#if UNITY_WEBGL
        foreach (GameObject go in _webGL)
        {
            go.SetActive(false);
        }
        _skillJoystickObj.transform.position = new Vector3(-100,0,0);

        foreach (GameObject go in _KeyInfo)
        {
            go.SetActive(true);
        }
#endif
    }
#if UNITY_ANDROID
    private void Update()
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
            else
            {
                float horizontal = _skillJoystick.Horizontal;
                float vertical = _skillJoystick.Vertical;
                
                //조이스틱 방향, 스킬, skillUse[index]
                if (vertical > 0.7 && horizontal * horizontal < 0.36)
                {
                    //상, 망치, 2
                    _skillIndex = 2;
                }
                else if(vertical < -0.7 && horizontal * horizontal < 0.36)
                {
                    //하, 사이코 메트릭, 3
                    _skillIndex = 3;
                }
                else if(vertical* vertical < 0.36 && horizontal < -0.7)
                {
                    //좌, 피부, 0
                    _skillIndex = 0;
                }
                else if (vertical * vertical < 0.36 && horizontal > 0.7)
                {
                    //우, 칼날, 1
                    _skillIndex = 1;
                }
                else if (vertical * vertical < 0.5 && horizontal * horizontal < 0.5)
                {
                    //스킬 사용 취소
                    _skillIndex = -1;
                }
            }
        }
        else
        {
            if (_skillJoystick.Horizontal != 0 || _skillJoystick.Vertical != 0)
            {
                isSkill = true;
                return;
            }

            if (!_isMove)
            {
                _moveJoystickObj.transform.localScale = Vector3.one;
                _skillJoystickObj.transform.localScale = Vector3.one;
                foreach (SkillUse skillUse in _skillUse)
                {
                    skillUse.NoDisplaySkill();
                }
                if(_skillIndex > -1)
                {
                    _skillUse[_skillIndex].UseSkill();
                    _skillIndex = -1;
                }
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
        }
    }
#endif
    public void SetConsumableItem(ItemConsumable itemConsumable)
    {
        _emptied.gameObject.SetActive(false);
        _consumableItem.sprite = itemConsumable.item.ItemIcon;
        _consumableItem.gameObject.SetActive(true);

    }

    public void DelConsumableItem()
    {
        _consumableItem.sprite = null;
        _consumableItem.gameObject.SetActive(false);
        _emptied.gameObject.SetActive(true);
        ItemManager.Instance.DelConsumable();
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

    public void OnInteractionButton()
    {
        if (_interactiveObject == null)
            return;

        _interactiveObject.GetComponent<InteractiveObject>().Use();
    }

    public void OnPickupButton()
    {
        if (ItemManager.Instance.pickupItem == null)
            return;

        ItemManager.Instance.pickupItem.Pickup();
        UIManager.Instance.CloseUI<UIItemPopup>().CloseItemPopup();
    }

    public void OnItemButton()
    {
        if (ItemManager.Instance.itemConsumable == null)
            return;

        ItemManager.Instance.itemConsumable.UseConsumable();
    }

    private void OnTalkButton()
    {

    }
    #endregion 버튼 클릭 이벤트

    #region 버튼 스위칭
    public void SwitchingAttack()
    {
#if UNITY_ANDROID
        _attackObj.SetActive(true);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(false);
        _talkObj.SetActive(false);
#endif
    }

    public void SwitchingInteraction()
    {
#if UNITY_ANDROID
        _attackObj.SetActive(false); 
        _interactionObj.SetActive(true);
        _pickupObj.SetActive(false);
        _talkObj.SetActive(false);
#endif
    }

    public void SwitchingPickup()
    {
#if UNITY_ANDROID
        _attackObj.SetActive(false);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(true);
        _talkObj.SetActive(false);
#endif
    }

    public void SwitchingTalk()
    {
#if UNITY_ANDROID
        _attackObj.SetActive(false);
        _interactionObj.SetActive(false);
        _pickupObj.SetActive(false); 
        _talkObj.SetActive(true);
#endif
    }
    #endregion 버튼 스위칭

}
