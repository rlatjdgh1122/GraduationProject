using ArmySystem;
using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
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
    [SerializeField] private SelectedStatusSlot _selectedStatusSlot = null;
    [SerializeField] private RectTransform _selectedTrm = null;

    private Dictionary<SynergyType, (UltimateType, Sprite, Sprite)> _synergyTypeToDataDic = new();
    private Dictionary<GeneralType, (SkillType, Sprite)> _generalTypeToDataDic = new();
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
            _synergyTypeToDataDic.Add(item.SynergyType, (item.UltimateType, item.UltimateIcon, item.SynergyIcon));


        }//end foreach

        foreach (var item in statusSlotRegisterSO.GeneralData)
        {
            _generalTypeToDataDic.Add(item.GeneralType, (item.SkillType, item.SkillIcon));

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
        var synergyData = _synergyTypeToDataDic[newArmy.SynergyType];

        SkillUI slotSkillUI = slot.SkillUI;
        UltimateUI slotUltimateUI = slot.UltimateUI;
        if (slotSkillUI)
        {
            var generalData = _generalTypeToDataDic[newArmy.General.GeneralType];
            _selectedStatusSlot.SetSkillUI(slotSkillUI.CurrntValue, slotSkillUI.CurrentFillAmount, generalData.Item1, generalData.Item2);
        }

        if (slotUltimateUI)
        {
            _selectedStatusSlot.SetUltimateUI(slotUltimateUI.CurrentFillAmount, synergyData.Item2);
        }

        _selectedStatusSlot.SetSynergyUI(synergyData.Item3);

        _selectedTrm.localPosition = new Vector3(-850, 150 + 65 * newArmy.LegionIdx, 0);
    }

    private void DestoryStatusSlot()
    {
        //지금은 오브젝트를 삭제해주는데 나중에 UI만 체인지 되게
        //오브젝트 지우는건 _armies의 카운트가 이전 카운트보다 적을때만
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

        foreach (var army in _armies)
        {
            StatusSlot slot = CreateSlot(army);
            var synergyData = GetDataBySynergyType(army.SynergyType);

            slot.SetSynergyUI(synergyData.Item3);
            if (army.General != null) //장군이 있을때
            {
                var generalData = GetDataByGeneralType(army.General.GeneralType);
                slot.SetSkillUI(generalData.Item1, generalData.Item2);

                if (army.IsSynergy) //시너지 활성화 될 때
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
        //army.GetInfo().Add(newSlot);

        return newSlot;
    }

    private (UltimateType, Sprite, Sprite) GetDataBySynergyType(SynergyType type) => _synergyTypeToDataDic[type];

    private (SkillType, Sprite) GetDataByGeneralType(GeneralType type) => _generalTypeToDataDic[type];

}
