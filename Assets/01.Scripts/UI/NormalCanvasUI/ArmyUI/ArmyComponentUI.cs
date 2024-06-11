using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyComponentUI : PopupUI
{
    [HideInInspector] public ArmyInfoPanel infoPanel;
    [HideInInspector] public GeneralSlotPanel generalSlotPanel;
    [HideInInspector] public DummyPenguinFactory factory;

    public static event Action<GeneralStat> OnShowGeneralInfo;
    public static event Action OnHideGeneralInfo;

    protected void ShowGeneralInfo(GeneralStat data)
    {
        OnShowGeneralInfo?.Invoke(data);
    }

    public void HideGeneralInfo()
    {
        OnHideGeneralInfo?.Invoke();
    }
}
