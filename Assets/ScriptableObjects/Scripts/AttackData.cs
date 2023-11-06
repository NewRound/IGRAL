using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "AttackData/Default")]
public class AttackData : ScriptableObject
{

    [field: SerializeField] public float[] AttackMod;
}
