using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactData", menuName = "SO/ItemData/Artifact")]

public class ArtifactSO : ItemBaseSO
{
    [field: Header("# Abilities")]
    [field: SerializeField] public float increaseDamageRate {  get; private set; }
    [field: SerializeField] public float increaseDefenseRate { get; private set; }
    [field: SerializeField] public float increaseSpeedRate {  get; private set; }
}
