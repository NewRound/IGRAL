using UnityEngine;
using UnityEngine.UI;

public class UISkillSlot : MonoBehaviour
{
    private SkillSO _skillSO;
    [SerializeField] private Button _skillButton;
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Image _bg;

    private bool _learned = false;
    private bool _unlock = false;

    public void InitSkillSlot(SkillSO skillSO)
    {
        _skillSO = skillSO;
        _skillIcon.sprite = skillSO.icon;
        _skillButton.onClick.AddListener(() => UISkillTree.Instance.SelectSkill(_skillSO));
        UpdateBg();
    }

    public void UpdateBg()
    {
        _learned = SkillManager.Instance.learnedSkills.ContainsKey(_skillSO.skillId);
        _unlock = _skillSO.unlockConditionId == "" ? true : SkillManager.Instance.learnedSkills.ContainsKey(_skillSO.unlockConditionId);

        if (_unlock && !_learned)
        {
            _bg.color = Color.white;
        }
        else if(_learned)
        {
            _bg.color = new Color(171/255f, 235/255f, 198/255f);
        }
    }

}
