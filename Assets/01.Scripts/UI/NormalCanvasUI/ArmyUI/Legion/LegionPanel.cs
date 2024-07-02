using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegionPanel : PopupUI
{
    public int LegionNumber;
    public string LegionName;

    public RectTransform PanelTrm => _rectTransform;
    public int CurrentIndex = 0;

    public int CurrentSlotCount => _soldierSlotList.Count;
    public EntityInfoDataSO SoldierlInfo; //일단 저장해놓음
    private List<LegionSoldierSlot> _soldierSlotList;
    public List<LegionSoldierSlot> SoldierSlotList => _soldierSlotList;

    [SerializeField] private RectTransform _lockedTrm;

    public override void Awake()
    {
        base.Awake();

        _soldierSlotList = GetComponentsInChildren<LegionSoldierSlot>()
                            .Where(slot => !slot.IsBonus)
                            .ToList();
    }

    public void UnlockedLegion() => _lockedTrm.gameObject.SetActive(false);

    public void SetSlots(EntityInfoDataSO info)
    {
        SoldierlInfo = info;
        int i = 0;
        foreach (LegionSoldierSlot slot in _soldierSlotList)
        {
            //여기서 자리를 예외처리해줌 일단은 이렇게 하고 나중에 수정
            slot.SetSlot(info, i++);
        }
    }

    public void SetLegionData(string name, int idx)
    {
        foreach (LegionSoldierSlot slot in _soldierSlotList)
        {
            slot.LegionName = name;
            slot.LegionIdx = idx;
        }
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }
}
