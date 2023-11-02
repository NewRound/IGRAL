using TMPro;
using UnityEngine;

public class UISkillCategory : MonoBehaviour
{
    private SkillDataSO _skillDatas;
    private UISkillSlot[] _slots;

    [Header("SkillCategory")]
    [SerializeField] private TextMeshProUGUI _skillCategoryName;
    [SerializeField] private GameObject _activate;

    [Header("SkillTree")]
    [SerializeField] private GameObject _uISkillSlot;
    [SerializeField] private Transform _content;
    public GameObject skillTree;
    
    public void SetSkillCategory(SkillDataSO skillDataSO)
    {
        _skillDatas = skillDataSO;
        _slots = new UISkillSlot[_skillDatas.skills.Length];
        _skillCategoryName.text = GetDescription.EnumToString(skillDataSO.skillCategoryType);
        int i = 0;
        foreach (SkillSO skillSO in _skillDatas.skills)
        {
            GameObject instantiate = Instantiate(_uISkillSlot, _content);
            UISkillSlot uISkillSlot = instantiate.GetComponent<UISkillSlot>();
            uISkillSlot.InitSkillSlot(skillSO);
            int index = i;
            _slots[index] = uISkillSlot;
            i++;
        }

        CloseCategory();
    }

    public virtual void OpenCategory()
    {
        _activate.SetActive(true);
        skillTree.SetActive(true);
    }

    public void CloseCategory()
    {
        _activate.SetActive(false);
        skillTree.SetActive(false);
    }

    public void UpdateCategory()
    {
        foreach (UISkillSlot uISkillSlot in _slots)
        {
            uISkillSlot.UpdateBg();
        }
    }
}
