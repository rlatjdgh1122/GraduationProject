using UnityEngine;

public abstract class CharacterStat : BaseStat
{
    [Header("Major stat")]
    public Stat strength; // 1����Ʈ�� ������ ����, ũ���� 1%
    public Stat agility; // 1����Ʈ�� ȸ�� 1%, ũ��Ƽ�� 1%
    public Stat intelligence; // 1����Ʈ�� ���������� 1����, �������� 3����, ��Ʈ �������� ������ 10% ����(����10�� ��Ʈ�� 10�� ����)
    public Stat vitality; // 1����Ʈ�� ü�� 5����.

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
}
