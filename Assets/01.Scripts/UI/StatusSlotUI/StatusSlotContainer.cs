using ArmySystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� UI���� �������ִ� Ŭ����
/// </summary>
/// 1. �������� �� ���õ� ��� ������ ������ ������
/// 2. ������ ���� �������� UI�� ����
/// 2-1. ó������ 1������ ���õǰ�
public class StatusSlotContainer : MonoBehaviour
{
    [SerializeField] private StatusSlotRegisterSO StatusSlotRegisterSO = null;
    [SerializeField] private SelectedStatusSlot _selectedStatusSlot = null;

    private Dictionary<SynergyType, StatusSlot> _synergyTypeToSlotDic = new();
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
        foreach (var item in StatusSlotRegisterSO.Data)
        {
            _synergyTypeToSlotDic.Add(item.SynergyType, item.Slot);

        }//end foreach
    }

    private void OnChangedArmyHandler(Army prevArmy, Army newArmy)
    {
        _selectedStatusSlot = _armyToSlotDic[newArmy] as SelectedStatusSlot;
        _selectedStatusSlot.SetArmy(prevArmy);
    }

    private void ApplyStatusSlot()
    {
        //������ ������Ʈ�� �������ִµ� ���߿� UI�� ü���� �ǰ�
        //������Ʈ ����°� _armies�� ī��Ʈ�� ���� ī��Ʈ���� ��������
        _armyToSlotDic.TryClear(p => Destroy(p));

        foreach (var item in _armies)
        {
            StatusSlot slot = CreateSlot(item);
            slot.SetArmy(item);
            slot.Init();

            _armyToSlotDic.Add(item, slot);

        }//end foreach
    }

    private StatusSlot CreateSlot(Army army)
    {
        var newSlot = GameObject.Instantiate(GetSlotBySynergyType(army.SynergyType), transform);
        newSlot.SetArmy(army);

        return newSlot;
    }

    private StatusSlot GetSlotBySynergyType(SynergyType type) => _synergyTypeToSlotDic[type];

}
