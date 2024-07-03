using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultBuilding : BaseBuilding
{
    [SerializeField] private LayerMask _defaultBuildingLayer;

    [SerializeField] DefaultBuildingType _defaultBuildingType;

    private Outline _outline;

    protected override void Awake()
    {
        base.Awake();
        _outline = GetComponent<Outline>();

        SetInstalled();
        InstalledGround()?.InstallBuilding();
    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase)
        {
            SpawnButton();
        }
    }

    private void OnMouseEnter()
    {
        OutlineActive(true);
    }

    private void OnMouseExit()
    {
        OutlineActive(false);
    }

    public void SpawnButton()
    {
        UIManager.Instance.ShowPanel("ArmyUI");
    }

    public void OutlineActive(bool value) => _outline.enabled = value;

    protected override void Running()
    {
    }
}
