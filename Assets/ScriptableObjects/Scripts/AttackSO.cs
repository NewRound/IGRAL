using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "AttackSO/Default")]
public class AttackSO : ScriptableObject
{

    [field: SerializeField] public float[] AttackMod;
}
