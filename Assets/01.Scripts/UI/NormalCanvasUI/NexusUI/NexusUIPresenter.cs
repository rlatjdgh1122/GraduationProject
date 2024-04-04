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
        _buildingViews.ForEach(view => view.nexus = nexusBase);
        //_buildingViews.ForEach(item => item.building = 
        //    nexusBase.BuildingDatabase.BuildingItems.FirstOrDefault(building => building.CodeName == item.name));
        foreach (BuildingItemInfo building in nexusBase.BuildingDatabase.BuildingItems)
        {
            if (nexusBase.NexusStat.level + 1 == building.UnlockedLevel)
            {
                nexusBase.NexusStat.previewBuilding = building;
            }
        }
    }

    #region NexusUI
    public void LevelUp()
    {
        nexusBase.NexusStat.maxHealth.AddSum
            (nexusBase.NexusStat.maxHealth.GetValue(), nexusBase.NexusStat.level, nexusBase.NexusStat.levelupIncreaseValue);
        nexusBase.NexusStat.level++;

        foreach (BuildingItemInfo building in nexusBase.BuildingDatabase.BuildingItems)
        {
            if (nexusBase.NexusStat.level == building.UnlockedLevel)
            {
                building.IsUnlocked = true;
                nexusBase.NexusStat.unlockedBuilding = building;
            }

            if (nexusBase.NexusStat.level + 1 == building.UnlockedLevel)
            {
                nexusBase.NexusStat.previewBuilding = building;
            }
        }

        nexusBase.HealthCompo.SetHealth(nexusBase.NexusStat);
        nexusBase.NexusStat.upgradePrice *= 2; // <-�̰� �ӽ�
        WorkerManager.Instance.MaxWorkerCount++; //�̰͵� �ӽü���
        SoundManager.Play2DSound(SoundName.LevelUp); //�̰͵� �ӽ�

        _buildingViews.ForEach(view => view.SetDefaultUI());

        OnUpdateNexusUI?.Invoke();

        if (TutorialManager.Instance.CurTutoQuestIdx == 4) //�ϴ� ����Ʈ
        {
            TutorialManager.Instance.CurTutorialProgressQuest();        
        }
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
            UIManager.Instance.ShowWarningUI("������ �����մϴ�");
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
