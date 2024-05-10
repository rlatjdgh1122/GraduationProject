using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StorePanel : PopupUI
{
    private Button _legionChangeButton;
    private LegionInventoryUI _legionUI;
    private PenguinSpawner _spawner;

    public override void Awake()
    {
        base.Awake();

        _legionChangeButton = transform.Find("LegionBtn").GetComponent<Button>();
        _legionUI = FindObjectOfType<LegionInventoryUI>();
        _spawner  = FindObjectOfType<PenguinSpawner>();

        _legionChangeButton.onClick.RemoveAllListeners();
        _legionChangeButton.onClick.AddListener(() => 
        {
            _legionUI.ShowLegionUIPanel();
            UIManager.Instance.HidePanel("StorePanel");
        });
    }

    public override void HidePanel()
    {
        base.HidePanel();

        _spawner.ChangeSpawnUIBool(false);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
