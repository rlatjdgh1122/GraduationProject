using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoldierSelectPanel : PopupUI
{
    [SerializeField] private GameObject _buttonExit;

    [SerializeField] private Transform _soliderPanel;
    private SoldierSelectSlot[] _slots;

    [HideInInspector] public LegionPanel currentPanel;

    [SerializeField]
    private TextMeshProUGUI[] _costTexts;

    public override void Awake()
    {
        base.Awake();

        _slots = _soliderPanel.GetComponentsInChildren<SoldierSelectSlot>();

        foreach(var text in _costTexts)
        {
            var legion = ExtensionMethod.FindParent<LegionPanel>(text.gameObject);
            text.text = legion.Cost.ToString();
        }
    }

    public void Setting(LegionPanel legion)
    {
        CostManager.Instance.SubtractFromCurrentCost(legion.Cost, () =>
        {
            currentPanel = legion;

            currentPanel.UnlockedLegion();

            foreach (SoldierSelectSlot slot in _slots)
            {
                slot.parentPanel = this;
                slot.SetButtonListener();
            }

            ShowPanel();
            _buttonExit.SetActive(false);
        });

        Army army = ArmyManager.Instance.CreateArmy();
        currentPanel.SetLegionName(army.LegionName);
        SetActive(true);
    }

    public void SetActive(bool active) => _soliderPanel.gameObject.SetActive(active);

    public override void HidePanel()
    {
        base.HidePanel();
        _buttonExit.SetActive(true);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
