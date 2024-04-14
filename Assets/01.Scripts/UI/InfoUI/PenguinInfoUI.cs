using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenguinInfoUI : PopupUI
{
    [SerializeField] private GameObject _statItemObject = null;
    [SerializeField] private int _statItemCount = 3;
    private EntityInfoDataSO _ownerInfoData => PenguinManager.Instance.GetCurrentInfoData;
    private BaseStat _ownerStat => PenguinManager.Instance.GetCurrentStat;

    #region 정보창 변수

    [Header("정보창 변수")]
    [SerializeField] private TextMeshProUGUI _weaponNameTxt = null;
    [SerializeField] private TextMeshProUGUI _penguinNameTxt = null;
    [SerializeField] private Image _penguinIcon = null;
    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    #endregion

    #region 스탯아이템 변수
    [Header("스탯창 변수")]
    [SerializeField] private Transform Attack_StatItemTrm = null;
    [SerializeField] private Transform Armor_StatItemTrm = null;

    private List<IStatable> _attackStatItemList = new();
    private int _attackStatCount = 0;

    private List<IStatable> _armorStatItemList = new();
    private int _armorStatCount = 0;
    #endregion

    private void Start()
    {
        SpawnAttackSlotItem(_statItemCount);
        SpawnArmorSlotItem(_statItemCount);
    }
    protected virtual void ShowStat()
    {
        ShowAttackStat(_ownerStat.damage);
        ShowAttackStat(_ownerStat.criticalChance);
        ShowAttackStat(_ownerStat.criticalValue);

        ShowAromorStat(_ownerStat.armor);
        ShowAromorStat(_ownerStat.maxHealth);
        ShowAromorStat(_ownerStat.evasion);
    }
    protected virtual void ShowInfo()
    {
        _weaponNameTxt.text = _ownerInfoData.Weapon;
        _penguinNameTxt.text = _ownerInfoData.PenguinName;
        _penguinIcon.sprite = _ownerInfoData.PenguinIcon;
        _legionNameTxt.text = _ownerInfoData.LegionName;
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
        ShowInfo();
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
        
        //정보창이 띄워져있는 상황에서 또 띄울때 예외처리해야됨
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
