using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMainUI : PopupUI
{
    [Header("Components")]
    public ArmyInfoPanel infoPanel;
    public GeneralSlotPanel generalSlotPanel;
    public LegionGeneralSelectPanel legionGeneralPanel;
    private DummyPenguinFactory _factory;

    public override void Awake()
    {
        base.Awake();

        _factory = GameObject.Find("PenguinSpawner/DummyPenguinFactory").GetComponent<DummyPenguinFactory>();

        ArmyComponentUI[] components = GetComponentsInChildren<ArmyComponentUI>();

        foreach (var compo in components)
        {
            compo.factory = _factory;
            compo.generalSlotPanel = generalSlotPanel;
            compo.legionGeneralSlot = legionGeneralPanel;
            //compo.infoPanel = infoPanel;
        }
    }
}
