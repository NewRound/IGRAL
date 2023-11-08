using UnityEngine;
using UnityEngine.UI;

public class UISkillSlot : MonoBehaviour
{
    private SkillInfoSO _skillInfoSO;
    [SerializeField] private Button _skillButton;
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Image _bg;

    private bool _learned = false;
    private bool _unlock = false;

    public void InitSkillSlot(SkillInfoSO skillInfoSO)
    {
        _skillInfoSO = skillInfoSO;
        _skillIcon.sprite = skillInfoSO.icon;
        _skillButton.onClick.AddListener(() => UISkillTree.Instance.SelectSkill(_skillInfoSO));
        UpdateBg();
    }

    public void UpdateBg()
    {
        _learned = SkillManager.Instance.learnedSkills.ContainsKey(_skillInfoSO.skillId);
        _unlock = _skillInfoSO.unlockConditionId == "" ? true : SkillManager.Instance.learnedSkills.ContainsKey(_skillInfoSO.unlockConditionId);

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
