using DG.Tweening;
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

    #region ����â ����

    [Header("����â ����")]
    [SerializeField] private TextMeshProUGUI _penguinTypeTxt = null;
    [SerializeField] private TextMeshProUGUI _penguinNameTxt = null;
    [SerializeField] private TextMeshProUGUI _penguinDescriptionTxt = null;
    [SerializeField] private TextMeshProUGUI _penguinPersonalityTxt = null;
    [SerializeField] private Image _penguinIcon = null;
    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    #endregion

    #region ���Ⱦ����� ����
    [Header("����â ����")]
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

        ShowAromorStat(_ownerStat.armor);
        ShowAromorStat(_ownerStat.maxHealth);
        ShowAromorStat(_ownerStat.evasion);
    }

    protected virtual void ShowInfo()
    {
        _penguinPersonalityTxt.text = _ownerInfoData.PenguinPersonality;
        _penguinDescriptionTxt.text = _ownerInfoData.PenguinDescription;
        _penguinTypeTxt.text = _ownerInfoData.PenguinTypeName;
        _penguinNameTxt.text = _ownerInfoData.PenguinName;
        _penguinIcon.sprite = _ownerInfoData.PenguinIcon;
        _legionNameTxt.text = _ownerInfoData.LegionName;
    }

    public void HidePenguinInfoUI()
    {
        UIManager.Instance.HidePanel("PenguinInfoUI");
    }

    public override void HidePanel()
    {
        base.HidePanel();
        Init();
        _rectTransform.DOScale(Vector3.zero, 0.5f);
        PenguinManager.Instance.DummyPenguinList.ForEach(p => p.OutlineCompo.enabled = false);
        PenguinManager.Instance.DummyPenguinCameraCompo.DisableCamera();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UIManager.Instance.HidePanel("StorePanel");

        _rectTransform.DOScale(Vector3.one, 0.9f);
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

        //����â�� ������ִ� ��Ȳ���� �� ��ﶧ ����ó���ؾߵ�
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
