using UnityEngine;

[System.Serializable]
public enum StatType
{
    MoveSpeed,
    AttackSpeed,
    Strength,
    Agility,
    Intelligence,
    Vitality,
    MaxHealth,
    Armor,
    MagicResistance,
    Damage,
    CriticalChance,
    CriticalDamage,
}
[System.Serializable]
public enum StatMode
{
    Increase,
    Decrease,
}

public abstract class BaseStat : ScriptableObject
{
    [Header("Default stats")]
    public Stat maxHealth; //체력
    public Stat armor; //방어도
    public Stat evasion; //회피도
    public Stat magicResistance; //마법방어

    public abstract int GetMaxHealthValue();
}
