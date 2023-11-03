
using System;

public enum StatChangeType
{
    Add,
    Reduce,
    Multiple,
    Override
}

[Serializable]
public class EquipMentStats 
{
    public StatChangeType statChangeType;

    public PlayerSO equipmentsSO;
}
