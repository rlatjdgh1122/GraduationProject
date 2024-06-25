using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelectPanel : PopupUI
{
    private SoldierSelectSlot[] _slots;
    [HideInInspector] public LegionPanel currentPanel;

    public override void Awake()
    {  
        base.Awake();

        _slots = GetComponentsInChildren<SoldierSelectSlot>();
    }

    public void Setting(LegionPanel legion)
    {
        currentPanel = legion;

        foreach (SoldierSelectSlot slot in _slots)
        {
            slot.parentPanel = this;
            slot.SetButtonListener();
        }

        ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
