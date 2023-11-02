using UnityEngine;

[CreateAssetMenu(fileName = "SkillDataSO", menuName = "SO/SkillData/SkillDataSO")]
public class SkillDataSO : ScriptableObject
{
    [field: SerializeField] public SkillCategoryType skillCategoryType { get; private set; }
    [field: SerializeField] public SkillSO[] skills { get; private set; }
}
