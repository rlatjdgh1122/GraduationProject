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

    private bool isSpawnUIOn;

    private Outline _outline;

    private ConstructionStation _constructionStation;


    public override void Init()
    {
        isSpawnUIOn = false;
    }

    protected override void Awake()
    {
        base.Awake();

        _constructionStation = FindAnyObjectByType<ConstructionStation>().GetComponent<ConstructionStation>();
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

    public void SpawnButton()
    {
        if (!UIManager.Instance.CheckShowAble(UIType.Store)) return;

        if (isSpawnUIOn)
        {
            UIManager.Instance.HidePanel("ArmyUI");
            ChangeSpawnUIBool(false);
        }
        else
        {
            UIManager.Instance.ShowPanel("ArmyUI");
            ChangeSpawnUIBool(true);
            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }

        if (_constructionStation.isSpawnUIOn)
        {
            _constructionStation.UpdateSpawnUIBool();
        }
    }

    public virtual void UpdateSpawnUIBool()
    {
        isSpawnUIOn = isSpawnUIOn ? false : true;

        BuildingOutline();
    }

    public void ChangeSpawnUIBool(bool value)
    {
        isSpawnUIOn = value;

        BuildingOutline();
    }

    private void BuildingOutline()
    {
        _outline.enabled = isSpawnUIOn;
    }

    protected override void Running()
    {
    }
}
