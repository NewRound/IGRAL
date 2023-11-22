using UnityEngine;

[CreateAssetMenu(fileName = "PhaseSO", menuName = "Boss/PhaseSO")]
public class PhaseSO : ScriptableObject
{
    [field: SerializeField] public PhaseInfo[] PhaseInfo { get; private set; }
}
