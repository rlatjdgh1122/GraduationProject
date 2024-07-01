using ArmySystem;
using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
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

    private Dictionary<SynergyType, Image> _synergyTypeToImageDic = new();
    private Dictionary<Army, StatusSlot> _armyToSlotDic = new();

    private List<Army> _armies => ArmyManager.Instance.Armies;

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= ApplyStatusSlot;
        SignalHub.OnArmyChanged -= OnChangedArmyHandler;
    }

    private void Awake()
    {
        SignalHub.OnBattlePhaseStartEvent += ApplyStatusSlot;
        SignalHub.OnArmyChanged += OnChangedArmyHandler;
    }

    private void Start()
    {
        foreach (var item in statusSlotRegisterSO.GeneralData)
        {
            _synergyTypeToImageDic.Add(item.SkillImage, item.);

        }//end foreach
    }

    private void OnChangedArmyHandler(Army prevArmy, Army newArmy) //
    {
        //_selectedStatusSlot = _armyToSlotDic[newArmy] as SelectedStatusSlot;
    }

    private void ApplyStatusSlot()
    {
        //������ ������Ʈ�� �������ִµ� ���߿� UI�� ü���� �ǰ�
        //������Ʈ ����°� _armies�� ī��Ʈ�� ���� ī��Ʈ���� ��������
        _armyToSlotDic.TryClear((k, v) =>
        {
            (k as IValueChanger<ArmyUIInfo>).Remove(v);
            Destroy(v);

        });

        foreach (var item in _armies)
        {
            StatusSlot slot = CreateSlot(item);
            slot.Init();

            _armyToSlotDic.Add(item, slot);

        }//end foreach
    }

    private StatusSlot CreateSlot(Army army)
    {
        Image synergyIamge = GetSlotBySynergyType(army.SynergyType);
        StatusSlot newSlot = Instantiate(statusSlotPrefab, slotperentTrm);
        army.GetInfo().Add(newSlot);

        return newSlot;
    }

    private Image GetSlotBySynergyType(SynergyType type) => _synergyTypeToImageDic[type];

}
