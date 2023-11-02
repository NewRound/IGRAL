using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SkillCategoryType
{
    [Description("피부")] Skin
    , [Description("칼날")] Knife
    , [Description("망치")] Hammer
    , [Description("사이코 메트릭")] Psychometric

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
    
    private Dictionary<string, int> _learnedSkills = new Dictionary<string, int>();
    private SkillSO _selectedSkill;

    private void Awake()
    {
        // 테스트 스킬 포인트
        _skillPoint = SkillManager.Instance.skillPoint;
        _learnedSkills = SkillManager.Instance.learnedSkills;
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
            SkillDataSO skillDataSO = Resources.Load<SkillDataSO>($"Skill/{enumItem}");
            instantiate.GetComponent<UISkillCategory>().SetSkillCategory(skillDataSO);
            _skillCategoryButtons[i] = instantiate.GetComponent<Button>();
            UISkillCategory uISkillCategory = instantiate.GetComponent<UISkillCategory>();
            _uISkillCategories[i] = uISkillCategory;
            int index = i;
            _skillCategoryButtons[i].onClick.AddListener(() => OnSkillCategory(index));
            _uISkillCategories[i].skillTree.transform.SetParent(_skillTreeInfo);
            i++;
        }
    }

    private void OnSkillCategory(int index)
    {
        for(int i = 0; i < (int)SkillCategoryType.Max; i++)
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

        _selectedSkillName.text = "";
        _selectedSkillDescription.text = "";
        
        _levelUpButton.gameObject.SetActive(false);
        _skillTree.SetActive(false);
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
        //Time.timeScale = 0f;
    }

    public void CloseSkillTree()
    {
        _skillTree.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void SelectSkill(SkillSO skillSO)
    {
        _selectedSkill = skillSO;

        _selectedSkillName.text = skillSO.skillName;
        _selectedSkillDescription.text = skillSO.skillDescription;

        _levelUpButton.gameObject.SetActive(false);
        if (!_learnedSkills.ContainsKey(skillSO.skillId))
        {
            string unlockConditionId = skillSO.unlockConditionId;

            if (unlockConditionId == "" || _learnedSkills.ContainsKey(skillSO.unlockConditionId))
            {
                _levelUpButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnLevelUpButton()
    {
        if(_selectedSkill.skPointUse <= _skillPoint)
        {
            _skillPoint -= _selectedSkill.skPointUse;
            if (!_learnedSkills.ContainsKey(_selectedSkill.skillId))
            {
                _learnedSkills.Add(_selectedSkill.skillId, 1);
            }
            else
            {
                _learnedSkills[_selectedSkill.skillId] += 1;
            }
        }
        UpdateSkillPoint();
        SelectSkill(_selectedSkill);
    }
}
