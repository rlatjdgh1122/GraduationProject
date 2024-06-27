using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegionGeneralSelectPanel : ArmyComponentUI
{
    [HideInInspector] public GeneralInfoDataSO GeneralInfo; //�ϴ� �����س���
    [SerializeField] private LegionGeneralSlot _generalSlot;

    public override void Awake()
    {
        base.Awake();
    }

    public void SetSlot(GeneralInfoDataSO info)
    {
        GeneralInfo = info;

        _generalSlot.SetSlot(GeneralInfo);
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        UpdateState();
    }
}
