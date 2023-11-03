using System;
using System.Collections.Generic;

public class SkillManager : CustomSingleton<SkillManager>
{
    public int skillPoint = 5;
    public Dictionary<string, int> baseSkills = new Dictionary<string, int>();
    public Dictionary<string, int> learnedSkills = new Dictionary<string, int>();
    private SkillUse[] _skillUse;

    private void Awake()
    {
        foreach (SkillCategoryType enumItem in Enum.GetValues(typeof(SkillCategoryType)))
        {
            baseSkills.Add($"{enumItem}",0);
        }
    }

    public void SetSkillUes(SkillUse[] skillUse)
    {
        _skillUse = skillUse;
    }

    public void UpdateLearn()
    {
        foreach(SkillUse skillUse in _skillUse)
        {
            if (learnedSkills.ContainsKey($"{skillUse.skillCategoryType}"))
            {
                skillUse.LearnedSkill();
            }
        }
    }
}
