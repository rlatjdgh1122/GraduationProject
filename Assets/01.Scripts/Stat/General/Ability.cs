using System;

public delegate void OnChangedValue(Ability ability);

[Serializable]
public class Ability 
{
    public OnChangedValue OnValidate;

    public string abilityName;
    public StatType statType;
    public StatMode statMode;
    public int value; // stat will statMode to %

    public int Value
    {
        set
        {
            this.value = value;
            OnValidate?.Invoke(this);
        }

        get => value;
    }
}   
