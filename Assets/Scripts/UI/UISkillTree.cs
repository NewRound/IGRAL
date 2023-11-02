using System;
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

    private void Awake()
    {
        // 테스트 스킬 포인트
        _skillPoint = 5;
        _closeButton.onClick.AddListener(CloseSkillTree);
        _skillCategoryButtons = new Button[(int)SkillCategoryType.Max];
        _uISkillCategories = new UISkillCategory[(int)SkillCategoryType.Max];
        int i = 0;
        foreach (SkillCategoryType enumItem in Enum.GetValues(typeof(SkillCategoryType)))
        {
            if (enumItem == SkillCategoryType.Max)
                return;

            GameObject instantiate = Instantiate(_skillCategory, _skillCategoryTap);
            instantiate.GetComponent<UISkillCategory>().SetSkillCategory(GetDescription.EnumToString(enumItem));
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
        Debug.Log($"{index} 클릭");
        for(int i = 0; i < (int)SkillCategoryType.Max; i++)
        {
            if(index == i)
            {
                _uISkillCategories[i].CategoryOpen();
            }
            else
            {
                _uISkillCategories[i].CategoryClose();
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

    public void UpdateSkillPoint(int add)
    {
        _skillPoint += add;
    }


    public void OpenSkillTree()
    {
        _skillTree.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void CloseSkillTree()
    {
        _skillTree.SetActive(false);
        //Time.timeScale = 1f;
    }



    private void SelectSkill()
    {

    }
}
