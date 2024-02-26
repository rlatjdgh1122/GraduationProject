

[System.Flags]
public enum StatType
{
    None                = 1 << 0,
    MoveSpeed           = 1 << 1,
    AttackSpeed         = 1 << 2,
    Strength            = 1 << 3,
    Agility             = 1 << 4,
    Intelligence        = 1 << 5,
    Vitality            = 1 << 6,
    MaxHealth           = 1 << 7,
    Armor               = 1 << 8,
    MagicResistance     = 1 << 9,
    Damage              = 1 << 10,
    CriticalChance      = 1 << 11,
    CriticalDamage      = 1 << 12,
}