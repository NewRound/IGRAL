using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SkillCategoryType
{
    [Description("�Ǻ�")] Skin
    , [Description("Į��")] Knife
    , [Description("��ġ")] Hammer
    , [Description("������ ��Ʈ��")] Psychometric

    , Max
}

public class UISkillTree : CustomSingleton<UISkillTree>
{
    [Header("SkillTree")]
    [SerializeField] private GameObject _skillTree;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TextMeshProUGUI _skillPointText;
    private int _skillPoint;

    [Header("SkillCategory")]
    [SerializeField] private GameObject _skillCategory;
    [SerializeField] private Transform _skillCategoryTap;
    [SerializeField] private Button[] _skillCategoryButtons;
    [SerializeField] private UISkillCategory[] _uISkillCategories;
    [SerializeField] private Transform _skillTreeInfo;
    private int _currentidx;

    [Header("SkillInfo")]
    [SerializeField] private TextMeshProUGUI _selectedSkillName;
    [SerializeField] private TextMeshProUGUI _selectedSkillDescription;
    [SerializeField] private Button _levelUpButton;
    [SerializeField] private TextMeshProUGUI _selectedSkillSituation;

    private Dictionary<string, int> _learnedSkills = new Dictionary<string, int>();
    private Dictionary<string, int> _baseSkills = new Dictionary<string, int>();
    private SkillInfoSO _selectedSkill;
    private int _selectedSkillCategoryIndex;

    private void Awake()
    {
        _closeButton.onClick.AddListener(CloseSkillTree);
        _levelUpButton.onClick.AddListener(OnLevelUpButton);
        _skillCategoryButtons = new Button[(int)SkillCategoryType.Max];
        _uISkillCategories = new UISkillCategory[(int)SkillCategoryType.Max];
        int i = 0;
        foreach (SkillCategoryType enumItem in Enum.GetValues(typeof(SkillCategoryType)))
        {
            if (enumItem == SkillCategoryType.Max)
                return;

            GameObject instantiate = Instantiate(_skillCategory, _skillCategoryTap);
            SkillSetSO skillSetSO = Resources.Load<SkillSetSO>($"Skill/{enumItem}");
            instantiate.GetComponent<UISkillCategory>().SetSkillCategory(skillSetSO);
            _skillCategoryButtons[i] = instantiate.GetComponent<Button>();
            UISkillCategory uISkillCategory = instantiate.GetComponent<UISkillCategory>();
            _uISkillCategories[i] = uISkillCategory;
            int index = i;
            _skillCategoryButtons[i].onClick.AddListener(() => OnSkillCategory(index));
            _uISkillCategories[i].skillTree.transform.SetParent(_skillTreeInfo);
            i++;

            switch (enumItem)
            {
                case SkillCategoryType.Skin:
                    SkillManager.Instance.skinData = Instantiate(skillSetSO.skillsData);
                    break;
                case SkillCategoryType.Knife:
                    SkillManager.Instance.knifeData = Instantiate(skillSetSO.skillsData);
                    break;
                case SkillCategoryType.Hammer:
                    SkillManager.Instance.hammerData = Instantiate(skillSetSO.skillsData);
                    break;
                case SkillCategoryType.Psychometric:
                    SkillManager.Instance.psychometricrData = Instantiate(skillSetSO.skillsData);
                    break;
            }

        }
    }

    private void OnSkillCategory(int index)
    {
        _selectedSkillCategoryIndex = index;
        InitSkillInfo();
        for (int i = 0; i < (int)SkillCategoryType.Max; i++)
        {
            if(index == i)
            {
                _uISkillCategories[i].OpenCategory();
            }
            else
            {
                _uISkillCategories[i].CloseCategory();
            }
        }
    }

    private void Start()
    {
        _skillPointText.text = _skillPoint.ToString();

        InitSkillInfo();
        _skillTree.SetActive(false);

        // �׽�Ʈ ��ų ����Ʈ
        _skillPoint = SkillManager.Instance.skillPoint;
        _learnedSkills = SkillManager.Instance.learnedSkills;
        _baseSkills = SkillManager.Instance.learnedSkills;
    }

    public void UpdateSkillPoint()
    {
        SkillManager.Instance.skillPoint = _skillPoint;
        _skillPointText.text = _skillPoint.ToString();
    }

    public void OpenSkillTree()
    {
        _skillTree.SetActive(true);
        UpdateSkillPoint();
        GameManager.Instance.StopGameTime();
    }

    public void CloseSkillTree()
    {
        _skillTree.SetActive(false);
        GameManager.Instance.PlayGameTime();
    }

    public void SelectSkill(SkillInfoSO skillInfoSO)
    {
        _selectedSkill = skillInfoSO;
        InitSkillInfo();

        _selectedSkillName.text = skillInfoSO.skillName;
        _selectedSkillDescription.text = skillInfoSO.skillDescription;

        _levelUpButton.gameObject.SetActive(false);
        if (!_learnedSkills.ContainsKey(skillInfoSO.skillId))
        {
            string unlockConditionId = skillInfoSO.unlockConditionId;

            if (unlockConditionId == "" || _learnedSkills.ContainsKey(skillInfoSO.unlockConditionId))
            {
                _levelUpButton.gameObject.SetActive(true);
            }
            else
            {
                _selectedSkillSituation.text = "�ش� ��ų�� ���� ��� �� �����ϴ�.";
            }
        }
        else
        {
            _selectedSkillSituation.text = "�ش� ��ų�� �̹� ������ϴ�.";
        }
    }

    private void InitSkillInfo()
    {
        _selectedSkillName.text = "";
        _selectedSkillDescription.text = "";
        _selectedSkillSituation.text = "";
        _levelUpButton.gameObject.SetActive(false);
    }

    private void OnLevelUpButton()
    {
        UIPopup uIPopup = UIManager.Instance.OpenUI<UIPopup>();
        uIPopup.SetPopup("��ų ������", $"{_selectedSkill.skillName}�� ���ðڽ��ϱ�?", LevelUp);
    }

    private void LevelUp()
    {
        if (_selectedSkill.skPointUse <= _skillPoint)
        {
            _skillPoint -= _selectedSkill.skPointUse;
            if (!_learnedSkills.ContainsKey(_selectedSkill.skillId))
            {
                _learnedSkills.Add(_selectedSkill.skillId, 1);
            }
            else
            {
                //TODO ��ų�� ������ �ִ� ���
                _learnedSkills[_selectedSkill.skillId] += 1;
            }
        }
        if (_baseSkills.ContainsKey(_selectedSkill.skillId))
        {
            SkillManager.Instance.UpdateLearn();
        }

        UpdateSkillPoint();
        SelectSkill(_selectedSkill);
        _uISkillCategories[_selectedSkillCategoryIndex].UpdateCategory();

        if (_selectedSkill.statChanges == null)
            return;

        if (_selectedSkill.statChanges.Length > 0)
        {
            SkillManager.Instance.UpdateSkillSO(_selectedSkill.skillCategoryType, _selectedSkill.statChanges);
        }

    }
}
