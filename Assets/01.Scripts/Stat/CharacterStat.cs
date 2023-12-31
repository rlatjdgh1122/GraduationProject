using UnityEngine;

public abstract class CharacterStat : BaseStat
{
    [Header("Major stat")]
    public Stat strength; // 1포인트당 데미지 증가, 크증뎀 1%
    public Stat agility; // 1포인트당 회피 1%, 크리티컬 1%
    public Stat intelligence; // 1포인트당 마법데미지 1증가, 마법저항 3증가, 도트 데미지에 지능의 10% 증뎀(지능10당 도트뎀 10퍼 증가)
    public Stat vitality; // 1포인트당 체력 5증가.

    [Header("Offensive stats")]
    public Stat damage;
    public Stat criticalChance;
    public Stat criticalDamage;

    public override int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + (vitality.GetValue() * 5);
    }

    public int GetDamage()
    {
        return damage.GetValue() + strength.GetValue();
    }
    public void AddStat(int value, StatType type, StatMode mode)
    {
        switch (type)
        {
            /*case StatType.MoveSpeed:
                if (mode == StatMode.Increase)
                {
                    moveSpeed.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    moveSpeed.AddDecrease(value);
                }
                break;

            case StatType.AttackSpeed:
                if (mode == StatMode.Increase)
                {
                    attackSpeed.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    attackSpeed.AddDecrease(value);
                }
                break;*/

            case StatType.Strength:
                if (mode == StatMode.Increase)
                {
                    strength.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    strength.AddDecrease(value);
                }
                break;

            case StatType.Agility:
                if (mode == StatMode.Increase)
                {
                    agility.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    agility.AddDecrease(value);
                }
                break;

            case StatType.Intelligence:
                if (mode == StatMode.Increase)
                {
                    intelligence.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    intelligence.AddDecrease(value);
                }
                break;

            case StatType.Vitality:
                if (mode == StatMode.Increase)
                {
                    vitality.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    vitality.AddDecrease(value);
                }
                break;

            case StatType.MaxHealth:
                if (mode == StatMode.Increase)
                {
                    maxHealth.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    maxHealth.AddDecrease(value);
                }
                break;

            case StatType.Armor:
                if (mode == StatMode.Increase)
                {
                    armor.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    armor.AddDecrease(value);
                }
                break;

            case StatType.MagicResistance:
                if (mode == StatMode.Increase)
                {
                    magicResistance.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    magicResistance.AddDecrease(value);
                }
                break;

            case StatType.Damage:
                if (mode == StatMode.Increase)
                {
                    damage.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    damage.AddDecrease(value);
                }
                break;

            case StatType.CriticalChance:
                if (mode == StatMode.Increase)
                {
                    criticalChance.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    criticalChance.AddDecrease(value);
                }
                break;

            case StatType.CriticalDamage:
                if (mode == StatMode.Increase)
                {
                    criticalDamage.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    criticalDamage.AddDecrease(value);
                }
                break;
        }
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        switch (type)
        {
           /* case StatType.MoveSpeed:
                if (mode == StatMode.Increase)
                {
                    moveSpeed.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    moveSpeed.RemoveDecrease(value);
                }
                break;

            case StatType.AttackSpeed:
                if (mode == StatMode.Increase)
                {
                    attackSpeed.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    attackSpeed.RemoveDecrease(value);
                }
                break;*/

            case StatType.Strength:
                if (mode == StatMode.Increase)
                {
                    strength.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    strength.RemoveDecrease(value);
                }
                break;

            case StatType.Agility:
                if (mode == StatMode.Increase)
                {
                    agility.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    agility.RemoveDecrease(value);
                }
                break;

            case StatType.Intelligence:
                if (mode == StatMode.Increase)
                {
                    intelligence.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    intelligence.RemoveDecrease(value);
                }
                break;

            case StatType.Vitality:
                if (mode == StatMode.Increase)
                {
                    vitality.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    vitality.RemoveDecrease(value);
                }
                break;

            case StatType.MaxHealth:
                if (mode == StatMode.Increase)
                {
                    maxHealth.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    maxHealth.RemoveDecrease(value);
                }
                break;

            case StatType.Armor:
                if (mode == StatMode.Increase)
                {
                    armor.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    armor.RemoveDecrease(value);
                }
                break;

            case StatType.MagicResistance:
                if (mode == StatMode.Increase)
                {
                    magicResistance.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    magicResistance.RemoveDecrease(value);
                }
                break;

            case StatType.Damage:
                if (mode == StatMode.Increase)
                {
                    damage.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    damage.RemoveDecrease(value);
                }
                break;

            case StatType.CriticalChance:
                if (mode == StatMode.Increase)
                {
                    criticalChance.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {   
                    criticalChance.RemoveDecrease(value);
                }
                break;

            case StatType.CriticalDamage:
                if (mode == StatMode.Increase)
                {
                    criticalDamage.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    criticalDamage.RemoveDecrease(value);
                }
                break;
        }
    }
}
