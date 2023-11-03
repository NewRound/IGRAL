using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SO/SkillData/SkillData")]
public class SkillSO : ScriptableObject
{
    [field: SerializeField] public string skillId { get; private set; }
    [field: SerializeField] public SkillCategoryType skillCategoryType { get; private set; }
    [field: SerializeField] public string skillName { get; private set; }
    [field: SerializeField] public string skillDescription { get; private set; }

    [field: SerializeField] public Sprite icon { get; private set; }

    [field: SerializeField] public string unlockConditionId { get; private set; }
    [field: SerializeField] public int skPointUse { get; private set; }
}
