using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelectPanel : PopupUI
{
    [SerializeField] private LegionNamingPanel _legionNamePanel;

    [SerializeField] private Transform _soliderPanel;
    private SoldierSelectSlot[] _slots;

    [HideInInspector] public LegionPanel currentPanel;

    public override void Awake()
    {  
        base.Awake();

        _slots = _soliderPanel.GetComponentsInChildren<SoldierSelectSlot>();
    }

    public void Setting(LegionPanel legion)
    {
        CostManager.Instance.SubtractFromCurrentCost(500 , () =>
        {
            currentPanel = legion;

            currentPanel.UnlockedLegion();

            _legionNamePanel.CurrentPanel = currentPanel;
            _legionNamePanel.ParentPanel = this;

            foreach (SoldierSelectSlot slot in _slots)
            {
                slot.parentPanel = this;
                slot.SetButtonListener();
            }

            ShowPanel();
            _legionNamePanel.ShowPanel();
            SetActive(false);
        });
    }

    public void SetActive(bool active) => _soliderPanel.gameObject.SetActive(active);

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
