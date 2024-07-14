using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMainUI : PopupUI
{
    [Header("Components")]
    public ArmyInfoPanel infoPanel;
    public GeneralSlotPanel generalSlotPanel;
    private DummyPenguinFactory _factory;
    private GameObject _buttonExit;

    public override void Awake()
    {
        base.Awake();

        _factory = GameObject.Find("PenguinSpawner/DummyPenguinFactory").GetComponent<DummyPenguinFactory>();
        _buttonExit = transform.Find("Panel/PopUp/ButtonExit").GetComponent<GameObject>();

        ArmyComponentUI[] components = GetComponentsInChildren<ArmyComponentUI>();

        foreach (var compo in components)
        {
            compo.factory = _factory;
            compo.buttonExit = _buttonExit;
            compo.generalSlotPanel = generalSlotPanel;
            //compo.infoPanel = infoPanel;
        }
    }

    public void HideArmyUI()
    {
        HidePanel();

        UIManager.Instance.ResetPanel();
    }
}
