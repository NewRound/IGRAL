using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public int skillId;
    public SkillCategoryType skillCategoryType;
    public string skillName;
    public string skillDescription;

    public Image icon;

    public int unlockConditionId;
    public int skPointUse;

    public bool learned = false;
    public bool locked = false;
}
