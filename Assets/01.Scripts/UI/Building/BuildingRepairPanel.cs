using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairPanel : BuildingUIComponent
{
    private SynergyBuildingInfoDataSO _info;

    private Image _buildingImage;
    private Image _hpBar;

    public override void Awake()
    {
        base.Awake();

        _hpBar = transform.Find("Fill").GetComponent<Image>();
        _buildingImage = transform.Find("BuildingImage").GetComponent<Image>();
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, panelFadeTime);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UpdateHealthBar();
        UpdateImage();
    }

    public void UpdateHealthBar()
    {
        _hpBar.DOFillAmount(buildingHealth.currentHealth / buildingHealth.maxHealth, 0.15f);
    }

    public void SetBuildingInfo(SynergyBuildingInfoDataSO info)
    {
        _info = info;
    }

    public void UpdateImage()
    {
        if (buildingHealth.IsDead)
            _buildingImage.sprite = _info.BrokenBuildingIcon;
        else
            _buildingImage.sprite = _info.BuildingIcon;
    }

    public void BuildingRepair()
    {
        _buildingImage.sprite = _info.BuildingIcon;
    }
}