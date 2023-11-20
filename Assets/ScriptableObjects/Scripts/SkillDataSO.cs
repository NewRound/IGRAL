using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SO/SkillData/SkillDataSO")]
public class SkillDataSO : PlayerSO
{
    [field: SerializeField] public float UsingKcal { get; private set; }
    [field: SerializeField] public float DurationKcal { get; private set; }
    [field: SerializeField] public float DurationTime { get; private set; }
}