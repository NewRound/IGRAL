using TMPro;
using UnityEngine;

public class UISkillCategory : MonoBehaviour
{
    private SkillSetSO _skillSets;
    private UISkillSlot[] _slots;

    [Header("SkillCategory")]
    [SerializeField] private TextMeshProUGUI _skillCategoryName;
    [SerializeField] private GameObject _activate;

    [Header("SkillTree")]
    [SerializeField] private GameObject _uISkillSlot;
    [SerializeField] private Transform _content;
    public GameObject skillTree;
    
    public void SetSkillCategory(SkillSetSO skillDataSO)
    {
        _skillSets = skillDataSO;
        _slots = new UISkillSlot[_skillSets.skills.Length];
        _skillCategoryName.text = GetDescription.EnumToString(skillDataSO.skillCategoryType);
        int i = 0;
        foreach (SkillInfoSO skillInfoSO in _skillSets.skills)
        {
            GameObject instantiate = Instantiate(_uISkillSlot, _content);
            UISkillSlot uISkillSlot = instantiate.GetComponent<UISkillSlot>();
            uISkillSlot.InitSkillSlot(skillInfoSO);
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
        GameManager.Instance.StopGameTime();
    }

    public void CloseCategory()
    {
        _activate.SetActive(false);
        skillTree.SetActive(false);
        GameManager.Instance.PlayGameTime();
    }

    public void UpdateCategory()
    {
        foreach (UISkillSlot uISkillSlot in _slots)
        {
            uISkillSlot.UpdateBg();
        }
    }
}
