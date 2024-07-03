using ArmySystem;
using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

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

    private Dictionary<SynergyType, (UltimateType, Sprite, Sprite)> _synergyTypeToDataDic = new();
    private Dictionary<GeneralType, (SkillType, Sprite)> _generalTypeToDataDic = new();
    private Dictionary<Army, StatusSlot> _armyToSlotDic = new();

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
    }

    private void Start()
    {
        foreach (var item in statusSlotRegisterSO.SynergyData)
        {
            _synergyTypeToDataDic.Add(item.SynergyType, (item.UltimateType, item.UltimateIcon, item.SynergyIcon));


        }//end foreach

        foreach (var item in statusSlotRegisterSO.GeneralData)
        {
            _generalTypeToDataDic.Add(item.GeneralType, (item.SkillType, item.SkillIcon));

        }//end foreach
    }

    private void OnChangedArmyHandler(Army prevArmy, Army newArmy)
    {
        //_selectedStatusSlot = _armyToSlotDic[newArmy] as SelectedStatusSlot;
    }

    private void DestoryStatusSlot()
    {
        //������ ������Ʈ�� �������ִµ� ���߿� UI�� ü���� �ǰ�
        //������Ʈ ����°� _armies�� ī��Ʈ�� ���� ī��Ʈ���� ��������
        _armyToSlotDic.TryClear((k, v) =>
        {
            (k as IValueChanger<ArmyUIInfo>).Remove(v);
            Destroy(v);

        });
    }

    private void ApplyStatusSlot()
    {
        foreach (var army in _armies)
        {
            StatusSlot slot = CreateSlot(army);
            var synergyData = GetDataBySynergyType(army.SynergyType);

            slot.SetSynergyUI(synergyData.Item3);
            if (army.General != null) //�屺�� ������
            {
                var generalData = GetDataByGeneralType(army.General.GeneralType);
                slot.SetSkillUI(generalData.Item1, generalData.Item2);

                if (army.IsSynergy) //�ó��� Ȱ��ȭ �� ��
                {
                    slot.SetUltimateUI(synergyData.Item1, synergyData.Item2);
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
        army.GetInfo().Add(newSlot);

        return newSlot;
    }

    private (UltimateType, Sprite, Sprite) GetDataBySynergyType(SynergyType type) => _synergyTypeToDataDic[type];

    private (SkillType, Sprite) GetDataByGeneralType(GeneralType type) => _generalTypeToDataDic[type];

}
