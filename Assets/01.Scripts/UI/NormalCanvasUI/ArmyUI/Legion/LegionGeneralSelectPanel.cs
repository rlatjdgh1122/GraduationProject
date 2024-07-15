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
    private int _generalSlotIdx = 0;

    private GeneralInfoDataSO _prevGeneralInfo = null;

    public override void Awake()
    {
        base.Awake();

        _legionPanel = transform.parent.GetComponent<LegionPanel>();
    }

    public void SetSlot(GeneralInfoDataSO info)
    {
        GeneralSelectSlotList.SetSelectedSlots(info);
        _prevGeneralInfo = GeneralInfo;
        GeneralInfo = info;
        JoinToArmy();
        _generalSlot.SetSlot(GeneralInfo);
        HidePanel();
    }

    private void JoinToArmy()
    {
        //전 장군은 빼줌
        if (_prevGeneralInfo != null)
        {
            DummyPenguin prevDummy = PenguinManager.Instance.FindGeneralDummyPenguin(_prevGeneralInfo.PenguinType);
            PenguinManager.Instance.RemoveDummy(prevDummy);
        }

        DummyPenguin dummy = PenguinManager.Instance.FindGeneralDummyPenguin(GeneralInfo.PenguinType);
        dummy.CloneInfo.SetLegionName(_legionPanel.LegionName);

        General general = ArmyManager.Instance.SpawnPenguin(dummy.CloneInfo, _generalSlotIdx) as General;
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
