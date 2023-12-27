using UnityEngine;

public abstract class BaseStat : ScriptableObject
{
    [Header("Stat")]
    public Stat moveSpeed; //
    public Stat attackSpeed; //
    [Header("Major stat")]
    public Stat strength; // 1����Ʈ�� ������ ����, ũ���� 1%
    public Stat agility; // 1����Ʈ�� ȸ�� 1%, ũ��Ƽ�� 1%
    public Stat intelligence; // 1����Ʈ�� ���������� 1����, �������� 3����, ��Ʈ �������� ������ 10% ����(����10�� ��Ʈ�� 10�� ����)
    public Stat vitality; // 1����Ʈ�� ü�� 5����.

    [Header("Defensive stats")]
    public Stat maxHealth; //ü��
    public Stat armor; //��
    public Stat evasion; //ȸ�ǵ�
    public Stat magicResistance; //�������

    [Header("Offensive stats")]
    public Stat damage;
    public Stat criticalChance;
    public Stat criticalDamage;

    protected Entity _owner;
    protected NexusBase _nexus;

    public abstract int GetDamage();

    public abstract int GetMaxHealthValue();
}
