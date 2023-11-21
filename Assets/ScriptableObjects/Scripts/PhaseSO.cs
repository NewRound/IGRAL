using UnityEngine;

[CreateAssetMenu(fileName = "PhaseSO", menuName = "Boss/PhaseSO")]
public class PhaseSO : ScriptableObject
{
    [SerializeField] private PhaseInfo[] phaseInfo;
}
