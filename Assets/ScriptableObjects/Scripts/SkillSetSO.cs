using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SO/SkillData/SkillSetSO")]
public class SkillSetSO : ScriptableObject
{
    [field: SerializeField] public SkillCategoryType skillCategoryType { get; private set; }
    [field: SerializeField] public SkillInfoSO[] skills { get; private set; }
    [field: SerializeField] public SkillDataSO skillsData { get; private set;}
}
