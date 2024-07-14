using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModifyLegionPanel : ArmyComponentUI
{
    [SerializeField] private LegionSwap _legionSwap;
    [SerializeField] private LegionNamingPanel _legionNamePanel;

    private TextMeshProUGUI _legionNameText;
    private Button _modifyLegionButton;

    private LegionPanel _currentPanel;

    public override void Awake()
    {
        base.Awake();

        _legionNameText = transform.Find("ModifyLegionName").GetComponent<TextMeshProUGUI>();
        //_modifyLegionButton = transform.Find("ModifyButton").GetComponent<Button>();

        //_modifyLegionButton.onClick.RemoveAllListeners();
        //_modifyLegionButton.onClick.AddListener(() => ModifyLegion());
    }

    private void OnEnable()
    {
        _legionSwap.OnSwapLegionEvent += SwapEvent;
        _legionNamePanel.OnLegionNameNamingEvent += ShowLegionName;
    }

    public void SwapEvent(LegionPanel currentPanel)
    {
        if (currentPanel.LegionName.Length > 0)
        {
            ShowLegionName(currentPanel);
        }
        else
        {
            HidePanel();
            return;
        }
    }

    public void ShowLegionName(LegionPanel currentPanel)
    {
        _legionNamePanel.CurrentPanel = currentPanel;

        _legionNameText.text = currentPanel.LegionName;
        _currentPanel = currentPanel;

        ShowPanel();
    }

    public void ModifyLegion()
    {
        _legionNamePanel.ModifyLegionName(_currentPanel.LegionName);
        _legionNamePanel.ShowPanel();
        //buttonExit.SetActive(false);
    }
}