using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LegionGeneralSelectPanel : ArmyComponentUI
{
    [HideInInspector] public GeneralInfoDataSO GeneralInfo; //일단 저장해놓음
    [SerializeField] private LegionGeneralSlot _generalSlot;

    public GeneralSelectSlotList GeneralSelectSlotList;
    private LegionPanel _legionPanel;

    public override void Awake()
    {
        base.Awake();

        _legionPanel = transform.parent.GetComponent<LegionPanel>();
    }

    public void SetSlot(GeneralInfoDataSO info)
    {
        GeneralSelectSlotList.SetSelectedSlots(info);
        GeneralInfo = info;
        CreateArmy();
        _generalSlot.SetSlot(GeneralInfo);
        HidePanel();
    }

    private void CreateArmy()
    {
        DummyPenguin dummy = PenguinManager.Instance.FindGeneralDummyPenguin(GeneralInfo.PenguinType);
        General general = ArrangementManager.Instance.SpawnPenguin(dummy.CloneInfo, 18) as General;
        PenguinManager.Instance.DummyToPenguinMapping(dummy, general);

        ArmyManager.Instance.JoinArmyToGeneral(_legionPanel.LegionName, _legionPanel.LegionIdx, general);
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
