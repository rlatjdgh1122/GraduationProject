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
    public NexusBase nexusBase;

    public override void Awake()
    {
        base.Awake();

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
