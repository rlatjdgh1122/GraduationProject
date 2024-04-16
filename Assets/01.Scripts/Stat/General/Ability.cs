using System;

[Serializable]
public class Ability 
{
    public string abilityName;
    public StatType statType;
    public StatMode statMode;
    public int value; // stat will statMode to %
}
