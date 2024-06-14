using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSlotPanel : ArmyComponentUI
{
    private GeneralSlot[] _slots;

    public override void Awake()
    {
        base.Awake();

        _slots = GetComponentsInChildren<GeneralSlot>();

        OnUpdateState += SetSlotUI;
    }

    private void Start()
    {
        SetSlotData();
    }

    public void SetSlotUI()
    {
        foreach (var slot in _slots)
        {
            slot.SetUI();
        }
    }

    private void SetSlotData()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (GeneralManager.Instance.GeneralList[i] != null)
                _slots[i].GeneralData = GeneralManager.Instance.GeneralList[i];
        }
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
