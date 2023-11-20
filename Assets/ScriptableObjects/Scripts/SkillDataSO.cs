using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SO/SkillData/SkillDataSO")]
public class SkillDataSO : PlayerSO
{
    [field: Header("Skill")]
    [field: SerializeField] public float UsingKcal { get; set; }
    [field: SerializeField] public float DurationKcal { get; set; }
    [field: SerializeField] public float DurationTime { get; set; }
}