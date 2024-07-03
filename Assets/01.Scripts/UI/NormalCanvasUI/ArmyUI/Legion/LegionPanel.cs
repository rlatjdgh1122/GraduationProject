using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegionPanel : PopupUI
{
    public int LegionIdx = 0;
    public string LegionName = "";

    public int SetLegionIdx(int legionIdx) => LegionIdx = legionIdx;
    public string SetLegionName(string legionName) => LegionName = legionName;


    public RectTransform PanelTrm => _rectTransform;
    public int CurrentIndex = 0;

    public int CurrentSlotCount => _soldierSlotList.Count;
    public EntityInfoDataSO SoldierlInfo; //�ϴ� �����س���
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
            //���⼭ �ڸ��� ����ó������ �ϴ��� �̷��� �ϰ� ���߿� ����
            slot.SetSlot(info, LegionName, i++);
        }
    }

    public void SetLegionData()
    {
        foreach (LegionSoldierSlot slot in _soldierSlotList)
        {
            slot.LegionName = LegionName;
            slot.LegionIdx = LegionIdx;
        }
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }
}