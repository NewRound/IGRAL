using System;

public enum StatChangeType
{
    Add
    , Subtract
    , Multiple
    , Division
    , Override
}

[Serializable]
public class EquipMentStats 
{
    public StatChangeType statChangeType;

    public PlayerSO equipmentsSO;
}
