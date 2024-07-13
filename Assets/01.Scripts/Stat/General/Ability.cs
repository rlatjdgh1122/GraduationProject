using System;



[Serializable]
public class Ability
{
    public Ability DeepCopy()
    {
        var instance = new Ability();

        instance.abilityName = abilityName;
        instance.statType = statType;
        instance.statMode = statMode;
        instance.value = value;

        return instance;
    }

    public OnValueUpdated<Ability> OnValidate;

    public string abilityName;
    public StatType statType;
    public StatMode statMode;
    public int value; // stat will statMode to %

    public bool isSkillUpgrade;

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
