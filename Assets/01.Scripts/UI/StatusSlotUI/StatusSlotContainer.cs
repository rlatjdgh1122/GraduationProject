using ArmySystem;
using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static GeneralSettingData;
using static SynergySettingData;

/// <summary>
/// ���� UI���� �������ִ� Ŭ����
/// </summary>
/// 1. �������� �� ���õ� ��� ������ ������ ������
/// 2. ������ ���� �������� UI�� ����
/// 2-1. ó������ 1������ ���õǰ�
public class StatusSlotContainer : MonoBehaviour
{
    [SerializeField] private StatusSlotRegisterSO statusSlotRegisterSO = null;
    [SerializeField] private StatusSlot statusSlotPrefab = null;
    [SerializeField] private Transform slotperentTrm = null;
    [SerializeField] private SelectedStatusSlot _selectedStatusSlot = null;
    [SerializeField] private RectTransform _selectedTrm = null;

    private Dictionary<SynergyType, SynergyData> _synergyTypeToSynergyInfoDic = new();
    private Dictionary<SynergyType, UltimateData> _synergyTypeToUltimateInfoDic = new();
    private Dictionary<GeneralType, SkillData> _generalTypeToSkillInfoDic = new();
    private Dictionary<Army, StatusSlot> _armyToSlotDic = new();

    private CanvasGroup _canvasGroup = null;
    private List<Army> _armies => ArmyManager.Instance.Armies;

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= ApplyStatusSlot;
        SignalHub.OnBattlePhaseEndEvent -= DestoryStatusSlot;
        SignalHub.OnArmyChanged -= OnChangedArmyHandler;
    }

    private void Awake()
    {
        SignalHub.OnBattlePhaseStartEvent += ApplyStatusSlot;
        SignalHub.OnBattlePhaseEndEvent += DestoryStatusSlot;
        SignalHub.OnArmyChanged += OnChangedArmyHandler;

        _canvasGroup = _selectedStatusSlot.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        foreach (var item in statusSlotRegisterSO.SynergyData)
        {
            _synergyTypeToSynergyInfoDic.Add(item.SynergyType, item.SynergyInfo);
            _synergyTypeToUltimateInfoDic.Add(item.SynergyType, item.UltimateInfo);

        }//end foreach

        foreach (var item in statusSlotRegisterSO.GeneralData)
        {
            _generalTypeToSkillInfoDic.Add(item.GeneralType, item.SkillInfo);

        }//end foreach
    }

    private void OnChangedArmyHandler(Army prevArmy, Army newArmy)
    {
        if (prevArmy != null)
        {
            prevArmy.GetInfo().Remove(_selectedStatusSlot);

        }

        newArmy.GetInfo().Add(_selectedStatusSlot);
        _selectedStatusSlot.SetArmy(newArmy);

        StatusSlot slot = _armyToSlotDic[newArmy];
        SynergyData synergyInfo = GetSynergyInfoBySynergyType(newArmy.SynergyType);
        UltimateData ultimateInfo = GetUltimateInfoBySynergyType(newArmy.SynergyType);

        SkillUI slotSkillUI = slot.SkillUI;
        UltimateUI slotUltimateUI = slot.UltimateUI;
        if (slotSkillUI)
        {
            var generalInfo = GetDataByGeneralType(newArmy.General.GeneralType);
            _selectedStatusSlot.SetSkillUI(slotSkillUI.CurrntValue, slotSkillUI.CurrentFillAmount, generalInfo);
        }

        if (slotUltimateUI)
        {
            _selectedStatusSlot.SetUltimateUI(slotUltimateUI.CurrentFillAmount, ultimateInfo);
        }

        _selectedStatusSlot.SetSynergyUI(synergyInfo);

        _selectedTrm.localPosition = new Vector3(-850, 150 + 65 * (_armies.Count - (newArmy.LegionIdx + 1)), 0);
    }

    private void DestoryStatusSlot()
    {
        //������ ������Ʈ�� �������ִµ� ���߿� UI�� ü���� �ǰ�
        //������Ʈ ����°� _armies�� ī��Ʈ�� ���� ī��Ʈ���� ��������
        _armyToSlotDic.TryClear((k, v) =>
        {
            //(k as IValueChanger<ArmyUIInfo>).Remove(v);
            Destroy(v.gameObject);
        });

        _canvasGroup.alpha = 0f;
        _selectedTrm.gameObject.SetActive(false);
    }

    private void ApplyStatusSlot()
    {
        _canvasGroup.alpha = 1f;
        _selectedTrm.gameObject.SetActive(true);
        _selectedStatusSlot.Init();

        foreach (var army in _armies)
        {
            StatusSlot slot = CreateSlot(army);
            SynergyData synergyInfo = GetSynergyInfoBySynergyType(army.SynergyType);
            UltimateData ultimateInfo = GetUltimateInfoBySynergyType(army.SynergyType);

            slot.SetSynergyUI(synergyInfo.SynergyIcon);
            if (army.General != null) //�屺�� ������
            {
                var generalInfo = GetDataByGeneralType(army.General.GeneralType);
                slot.SetSkillUI(generalInfo.SkillType, generalInfo.SkillIcon);

                if (army.IsSynergy) //�ó��� Ȱ��ȭ �� ��
                {
                    slot.SetUltimateUI(ultimateInfo.UltimateType, ultimateInfo.UltimateIcon);
                }
            }

            slot.SetArmy(army);
            slot.Init();

            _armyToSlotDic.Add(army, slot);

        }//end foreach
    }

    private StatusSlot CreateSlot(Army army)
    {
        StatusSlot newSlot = Instantiate(statusSlotPrefab, slotperentTrm);
        //army.GetInfo().Add(newSlot);

        return newSlot;
    }

    private SynergyData GetSynergyInfoBySynergyType(SynergyType type) => _synergyTypeToSynergyInfoDic[type];
    private UltimateData GetUltimateInfoBySynergyType(SynergyType type) => _synergyTypeToUltimateInfoDic[type];
    private SkillData GetDataByGeneralType(GeneralType type) => _generalTypeToSkillInfoDic[type];

}
