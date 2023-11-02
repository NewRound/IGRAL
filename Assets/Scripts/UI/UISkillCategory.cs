using TMPro;
using UnityEngine;

public class UISkillCategory : MonoBehaviour
{
    private SkillDataSO _skillDatas;
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

        _skillCategoryName.text = GetDescription.EnumToString(skillDataSO.skillCategoryType);

        foreach (SkillSO skillSO in _skillDatas.skills)
        {
            GameObject instantiate = Instantiate(_uISkillSlot, _content);
            instantiate.GetComponent<UISkillSlot>().InitSkillSlot(skillSO);
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
}
