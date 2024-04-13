using System.Collections.Generic;
using UnityEngine;

public class PenguinInfoUI : PopupUI
{
    [SerializeField] private GameObject _statItemObject = null;
    [SerializeField] private int _statItemCount = 3;
    private BaseStat _ownerStat => PenguinManager.Instance.GetCurrentStat;
    private EntityInfoDataSO _ownerInfoData => PenguinManager.Instance.GetCurrentInfoData;

    [SerializeField] private Transform Attack_StatItemTrm = null;
    [SerializeField] private Transform Armor_StatItemTrm = null;

    private List<IStatable> _attackStatItemList = new();
    private int _attackStatCount = 0;

    private List<IStatable> _armorStatItemList = new();
    private int _armorStatCount = 0;

    protected virtual void ShowStat()
    {
        ShowAttackStat(_ownerStat.damage);
        ShowAttackStat(_ownerStat.criticalChance);
        ShowAttackStat(_ownerStat.criticalValue);

        ShowAromorStat(_ownerStat.armor);
        ShowAromorStat(_ownerStat.maxHealth);
        ShowAromorStat(_ownerStat.evasion);
    }
    private void Start()
    {
        SpawnAttackSlotItem(_statItemCount);
        SpawnArmorSlotItem(_statItemCount);
    }
    public override void HidePanel()
    {
        base.HidePanel();

        Init();
    }
    public override void ShowPanel()
    {
        base.ShowPanel();

        ShowStat();
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
