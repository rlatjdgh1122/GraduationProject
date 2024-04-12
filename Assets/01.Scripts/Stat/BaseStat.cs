using System.Collections.Generic;
using UnityEngine;

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

    [Header("Major stat")]
    public Stat strength; // 1����Ʈ�� ������ ����, ũ���� 1%
    public Stat agility; // 1����Ʈ�� ȸ�� 1%, ũ��Ƽ�� 1%
    public Stat intelligence; // 1����Ʈ�� ���������� 1����, �������� 3����, ��Ʈ �������� ������ 10% ����(����10�� ��Ʈ�� 10�� ����)
    public Stat vitality; // 1����Ʈ�� ü�� 5����.

    [Header("Offensive stats")]
    public Stat damage;
    public Stat criticalChance;
    public Stat criticalValue;

    private Dictionary<Stat, string> statTypeToNameDic;
    public void Init()
    {
        statTypeToNameDic = new()
        {
            { maxHealth,         "�ִ�ü��"},
            { armor,             "����"},
            { evasion,           "ȸ�� Ȯ��"},

            { damage,            "���ݷ�"},
            { criticalChance,    "ũ��Ƽ�� Ȯ��"},
            { criticalValue,     "ũ��Ƽ�� �߰� ������"},

        };
    }
    public string GetStatNameByStat(Stat stat)
    {
        if (statTypeToNameDic.TryGetValue(stat, out string name))
        {
            return name;
        }
        Debug.Log("�ش��ϴ� ������ �̸��� ��ϵ��� �ʾҽ��ϴ�.");

        return "��ϵ��� ���� ����";
    }
    public virtual int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + (vitality.GetValue() * 5);
    }
    public int GetDamage()
    {
        return damage.GetValue() + strength.GetValue();
    }

    /// <summary>
    /// �ش��ϴ� Ÿ�� ����
    /// </summary>
    /// <param name="type"> ���� Ÿ��</param>
    /// <param name="mode"> (��� ���� �Ǵ� ���� ����)</param>
    public void ResetStatByStatType(StatType type, StatMode mode)
    {
        switch (type)
        {
            case StatType.Strength:
                if (mode == StatMode.Increase)
                {
                    strength.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    strength.StatDecReset();
                }
                break;

            case StatType.Agility:
                if (mode == StatMode.Increase)
                {
                    agility.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    agility.StatDecReset();
                }
                break;

            case StatType.Intelligence:
                if (mode == StatMode.Increase)
                {
                    intelligence.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    intelligence.StatDecReset();
                }
                break;

            case StatType.Vitality:
                if (mode == StatMode.Increase)
                {
                    vitality.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    vitality.StatDecReset();
                }
                break;

            case StatType.MaxHealth:
                if (mode == StatMode.Increase)
                {
                    maxHealth.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    maxHealth.StatDecReset();
                }
                break;

            case StatType.Armor:
                if (mode == StatMode.Increase)
                {
                    armor.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    armor.StatDecReset();
                }
                break;

            case StatType.MagicResistance:
                if (mode == StatMode.Increase)
                {
                    magicResistance.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    magicResistance.StatDecReset();
                }
                break;

            case StatType.Damage:
                if (mode == StatMode.Increase)
                {
                    damage.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    damage.StatDecReset();
                }
                break;

            case StatType.CriticalChance:
                if (mode == StatMode.Increase)
                {
                    criticalChance.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    criticalChance.StatDecReset();
                }
                break;

            case StatType.CriticalDamage:
                if (mode == StatMode.Increase)
                {
                    criticalValue.StatIncReset();
                }
                else if (mode == StatMode.Decrease)
                {
                    criticalValue.StatDecReset();
                }
                break;
        }
    }

    /// <summary>
    /// �ش�Ǵ� ��� ����
    /// </summary>
    /// <param name="mode"> (��� ���� �Ǵ� ���� ����)</param>
    public void ResetStatByStatMode(StatMode mode)
    {
        if (mode == StatMode.Increase)
        {
            maxHealth.StatIncReset();
            armor.StatIncReset();
            evasion.StatIncReset();
            magicResistance.StatIncReset();
            strength.StatIncReset();
            agility.StatIncReset();
            intelligence.StatIncReset();
            vitality.StatIncReset();
            damage.StatIncReset();
            criticalChance.StatIncReset();
            criticalValue.StatIncReset();
        }
        else
        {
            maxHealth.StatDecReset();
            armor.StatDecReset();
            evasion.StatDecReset();
            magicResistance.StatDecReset();
            strength.StatDecReset();
            agility.StatDecReset();
            intelligence.StatDecReset();
            vitality.StatDecReset();
            damage.StatDecReset();
            criticalChance.StatDecReset();
            criticalValue.StatDecReset();
        }
    }

    /// <summary>
    /// ��� ���� ����(���� �ʱ�ȭ)
    /// </summary>
    public void ResetAllStat()
    {
        maxHealth.StatAllReset();
        armor.StatAllReset();
        evasion.StatAllReset();
        magicResistance.StatAllReset();
        strength.StatAllReset();
        agility.StatAllReset();
        intelligence.StatAllReset();
        vitality.StatAllReset();
        damage.StatAllReset();
        criticalChance.StatAllReset();
        criticalValue.StatAllReset();
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
                    criticalValue.AddIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    criticalValue.AddDecrease(value);
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
                    criticalValue.RemoveIncrease(value);
                }
                else if (mode == StatMode.Decrease)
                {
                    criticalValue.RemoveDecrease(value);
                }
                break;
        }
    }
}
