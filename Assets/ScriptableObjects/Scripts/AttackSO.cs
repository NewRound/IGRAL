using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "AttackSO/Default")]
public class AttackSO : ScriptableObject
{
    [field: SerializeField] public float[] AttackMod;
}
