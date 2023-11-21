using System;
using UnityEngine;

[Serializable]
public class PhaseInfo
{
    [field: SerializeField] public int PhaseNum { get; private set; } = 1;
    [field: SerializeField] public GameObject weaponPrefab { get; private set; }
    [field: SerializeField] public Pattern pattern { get; private set; }
    [field: SerializeField] public float SkillCoolTime { get; private set; }

}