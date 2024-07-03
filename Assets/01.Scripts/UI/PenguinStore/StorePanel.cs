using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StorePanel : PopupUI
{
    private Button _legionChangeButton;
    private LegionInventoryUI _legionUI;

    public override void Awake()
    {
        base.Awake();

        _legionChangeButton = transform.Find("LegionBtn").GetComponent<Button>();
        _legionUI = FindObjectOfType<LegionInventoryUI>();

        _legionChangeButton.onClick.RemoveAllListeners();
        _legionChangeButton.onClick.AddListener(() => 
        {
            UIManager.Instance.HidePanel("StorePanel");
            _legionUI.ShowLegionUIPanel();
        });
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
