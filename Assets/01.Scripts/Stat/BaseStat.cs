using UnityEngine;

public abstract class BaseStat : ScriptableObject
{
    [Header("Stat")]
    public Stat moveSpeed; //
    public Stat attackSpeed; //
    [Header("Major stat")]
    public Stat strength; // 1포인트당 데미지 증가, 크증뎀 1%
    public Stat agility; // 1포인트당 회피 1%, 크리티컬 1%
    public Stat intelligence; // 1포인트당 마법데미지 1증가, 마법저항 3증가, 도트 데미지에 지능의 10% 증뎀(지능10당 도트뎀 10퍼 증가)
    public Stat vitality; // 1포인트당 체력 5증가.

    [Header("Defensive stats")]
    public Stat maxHealth; //체력
    public Stat armor; //방어도
    public Stat evasion; //회피도
    public Stat magicResistance; //마법방어

    [Header("Offensive stats")]
    public Stat damage;
    public Stat criticalChance;
    public Stat criticalDamage;

    protected Entity _owner;
    protected NexusBase _nexus;

    public abstract int GetDamage();

    public abstract int GetMaxHealthValue();
}
