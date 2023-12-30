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
    public Stat maxHealth; //ü��
    public Stat armor; //��
    public Stat evasion; //ȸ�ǵ�
    public Stat magicResistance; //�������

    public abstract int GetMaxHealthValue();
}
