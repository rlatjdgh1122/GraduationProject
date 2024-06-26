using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegionPanel : PopupUI
{
    public RectTransform PanelTrm => _rectTransform;
    public int CurrentIndex = 0;

    public int CurrentSlotCount => _soldierSlotList.Count;
    private EntityInfoDataSO _info; //일단 저장해놓음
    private List<LegionSoldierSlot> _soldierSlotList;

    public override void Awake()
    {
        base.Awake();

        _soldierSlotList = GetComponentsInChildren<LegionSoldierSlot>().ToList();
    }

    public void SetSlots(EntityInfoDataSO info)
    {
        _info = info;

        foreach (LegionSoldierSlot slot in _soldierSlotList)
        {
            slot.SetSlot(info);
        }
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }
}
