using ArmySystem;
using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스탯 UI들을 관리해주는 클래스
/// </summary>
/// 1. 전투시작 시 선택된 모든 군단의 정보를 가져옴
/// 2. 가져온 것을 바탕으로 UI를 생성
/// 2-1. 처음에는 1군단이 선택되게
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
        //지금은 오브젝트를 삭제해주는데 나중에 UI만 체인지 되게
        //오브젝트 지우는건 _armies의 카운트가 이전 카운트보다 적을때만
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
