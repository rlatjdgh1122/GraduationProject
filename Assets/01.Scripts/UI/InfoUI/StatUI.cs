using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatUI : PopupUI
{
    [SerializeField] protected GameObject _statItemObject = null;
    [SerializeField] protected int _statItemCount = 3;
    protected BaseStat _ownerStat => PenguinManager.Instance.GetCurrentStat;

    [SerializeField] private Transform Attack_StatItemTrm = null;
    [SerializeField] private Transform Armor_StatItemTrm = null;

    private List<IStatable> _attackStatItemList = new();
    private int _attackStatCount = 0;

    private List<IStatable> _armorStatItemList = new();
    private int _armorStatCount = 0;

    private void Start()
    {
        SpawnAttackSlotItem(2);
        SpawnArmorSlotItem(4);
    }

    protected virtual void ShowStat()
    {
        ShowAttackStat(_ownerStat.damage);
        ShowAttackStat(_ownerStat.criticalChance);

        ShowAromorStat(_ownerStat.maxHealth);
        ShowAromorStat(_ownerStat.armor);
        ShowAromorStat(_ownerStat.evasion);
        ShowAromorStat(_ownerStat.tenacity);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        ShowStat();
    }

    public override void HidePanel()
    {
        base.HidePanel();
        Init();
    }

    private void SpawnAttackSlotItem(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            StatItem statItem = new StatItem(_statItemObject, Attack_StatItemTrm);
            _attackStatItemList.Add(statItem);
        }
    }

    private void SpawnArmorSlotItem(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            StatItem statItem = new StatItem(_statItemObject, Armor_StatItemTrm);
            _armorStatItemList.Add(statItem);
        }
    }

    private void ShowAttackStat(Stat stat)
    {
        string statName = _ownerStat.GetStatNameByStat(stat);

        _attackStatItemList[_attackStatCount].Modify(stat, statName);

        ++_attackStatCount;
    }

    private void ShowAromorStat(Stat stat)
    {
        string statName = _ownerStat.GetStatNameByStat(stat);
        _armorStatItemList[_armorStatCount].Modify(stat, statName);

        ++_armorStatCount;
    }

    private void Init()
    {
        _attackStatCount = 0;
        _armorStatCount = 0;
    }
}
