using ArmySystem;
using Define.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class LegionPanel : PopupUI
{
    public int Cost = 0;
    public int LegionIdx = 0;

    private string _legionName = string.Empty;

    public string LegionName { get => _legionName; private set { _legionName = value; } }

    public int SetLegionIdx(int legionIdx) => LegionIdx = legionIdx;
    public string SetLegionName(string legionName) => LegionName = legionName;

    public RectTransform PanelTrm => _rectTransform;
    public int CurrentIndex = 0;

    public int CurrentSlotCount => _soldierSlotList.Count;
    public EntityInfoDataSO SoldierlInfo; //일단 저장해놓음
    private List<LegionSoldierSlot> _soldierSlotList;
    public List<LegionSoldierSlot> SoldierSlotList => _soldierSlotList;

    [SerializeField] private RectTransform _lockedTrm;

    [SerializeField] private LegionHealingPanel _healPanel;

    public Sprite SkullIcon { get; private set; } = null;

    private void OnDestroy()
    {
        //SignalHub.OnBattlePhaseEndEvent -= OnBattleEndEventHandler;
    }

    public override void Awake()
    {
        base.Awake();

        _soldierSlotList = GetComponentsInChildren<LegionSoldierSlot>()
                            .Where(slot => !slot.IsBonus)
                            .ToList();

        //SignalHub.OnBattlePhaseEndEvent += OnBattleEndEventHandler;
    }

    private void Start()
    {
        SkullIcon = VResources.Load<Sprite>("Skull");
    }


    private void OnBattleEndEventHandler()
    {
        Army army = ArmyManager.Instance.GetArmyByLegionName(LegionName);

        if (army != null)
        {
            int deadCount = army.DeadPenguins.Count;

            try
            {
                _soldierSlotList.ForEach(s => s.Icon.sprite = SoldierlInfo.PenguinIcon); //일단 초기화
                _soldierSlotList.ApplyToCount(deadCount, s => s.Icon.sprite = SkullIcon);
            }
            catch (Exception ex)
            {
                Debug.LogError($"LegionPanel의 OnBattleEndEventHandler에서 {ex}");
            }

        }
    }

    public void UnlockedLegion() => _lockedTrm.gameObject.SetActive(false);

    public void SetSlots(EntityInfoDataSO info)
    {
        SoldierlInfo = info;

        foreach (LegionSoldierSlot slot in _soldierSlotList)
        {
            //여기서 자리를 예외처리해줌 일단은 이렇게 하고 나중에 수정
            slot.SetSlot(info, LegionName, LegionIdx);
        }
    }

    public void SetLegionData()
    {
        /*foreach (LegionSoldierSlot slot in _soldierSlotList)
        {
            slot.LegionName = LegionName;
            slot.LegionIdx = LegionIdx;
        }*/
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }

    public void Heal<T>(T penguin, EntityInfoDataSO infoData, Action action) where T : Penguin
    {
        _healPanel.HealingPenguin(penguin, infoData,action);
    }
}
