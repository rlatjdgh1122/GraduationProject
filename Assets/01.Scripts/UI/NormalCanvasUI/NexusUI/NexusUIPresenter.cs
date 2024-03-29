using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NexusUIPresenter : NexusPopupUI
{
    #region events
    public Action OnUpdateNexusUI;
    #endregion

    [SerializeField]
    private BuildingDatabaseSO _buildingDatabase;
    private List<BuildingView> _buildingViews;

    [HideInInspector]
    public BuildingType buildingType;
    public NexusBase nexusBase;

    private BuildingFactory _buildingFactory;

    public override void Awake()
    {
        base.Awake();

        _buildingFactory = FindAnyObjectByType<BuildingFactory>();

        _buildingViews = GetComponentsInChildren<BuildingView>().ToList();
        _buildingViews.ForEach(item => item.building = 
            _buildingDatabase.BuildingItems.FirstOrDefault(building => building.CodeName == item.name));
    }

    #region NexusUI
    public void LevelUp()
    {
        nexusBase.NexusStat.maxHealth.AddSum
            (nexusBase.NexusStat.maxHealth.GetValue(), nexusBase.NexusStat.level, nexusBase.NexusStat.levelupIncreaseValue);
        nexusBase.NexusStat.level++;
        nexusBase.NexusStat.upgradePrice *= 2; // <-이건 임시
        WorkerManager.Instance.MaxWorkerCount++; //이것도 임시수식

        OnUpdateNexusUI?.Invoke();
    }

    public void OnAdmitBuildingPanel()
    {
        UIManager.Instance.MovePanel("NexusPanel", -2500, 0, 0.7f);
        UIManager.Instance.MovePanel("BuildingPanel", 0, 0, 0.7f);
    }

    public void OnAdmitNexusPanel()
    {
        UIManager.Instance.MovePanel("NexusPanel", 0, 0, 0.7f);
        UIManager.Instance.MovePanel("BuildingPanel", 2500, 0, 0.7f);
    }
    #endregion

    #region buildingUI
    public void PurchaseBuilding(BuildingView view)
    {
        if (CostManager.Instance.Cost >= view.building.Price)
        {
            if (view.building.IsUnlocked)
            {
                view.spawn.SetUpButtonInfo(view.purchaseButton, _buildingFactory, view.building);
            }
        }
        else
        {
            UIManager.Instance.ShowWarningUI("생선이 부족합니다");
        }
    }
    #endregion

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
