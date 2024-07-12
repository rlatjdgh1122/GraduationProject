using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairPanel : BuildingUIComponent
{
    private Image _hpBar;

    public override void Awake()
    {
        base.Awake();

        _hpBar = transform.Find("Fill").GetComponent<Image>();
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, panelFadeTime);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        _hpBar.DOFillAmount(buildingHealth.currentHealth / buildingHealth.maxHealth, 0.15f);
    }
}