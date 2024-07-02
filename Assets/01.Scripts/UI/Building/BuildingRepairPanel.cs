using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairPanel : BuildingUIComponent
{
    private Image _fillBar;

    public override void Awake()
    {
        base.Awake();

        _fillBar = transform.Find("Fill").GetComponent<Image>();
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, _panelFadeTime);
        ShowHpBar();
    }

    private void ShowHpBar()
    {
        _fillBar.DOFillAmount(buildingHealth.currentHealth / buildingHealth.maxHealth, 0.5f);
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        ShowHpBar();
    }
}
