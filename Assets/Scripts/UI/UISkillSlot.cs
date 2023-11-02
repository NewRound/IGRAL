using UnityEngine;
using UnityEngine.UI;

public class UISkillSlot : MonoBehaviour
{
    private SkillSO _skillSO;
    [SerializeField] private Button _skillButton;
    [SerializeField] private Image _skillIcon;

    private bool _learned = false;


    public void InitSkillSlot(SkillSO skillSO)
    {
        _skillSO = skillSO;
        _skillIcon.sprite = skillSO.icon;
        _learned = SkillManager.Instance.learnedSkills.ContainsKey(skillSO.skillId);
        _skillButton.onClick.AddListener(() => UISkillTree.Instance.SelectSkill(_skillSO));
    }

}
