using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NexusUIPresenter : NexusPopupUI
{
    [HideInInspector]
    public BuildingType buildingType;

    private BuildingFactory _buildingFactory;

    public override void Awake()
    {
        base.Awake();

        _buildingFactory = FindAnyObjectByType<BuildingFactory>();
    }

    #region NexusUI
    public void LevelUp()
    {
        _nexusStat.maxHealth.AddSum
            (_nexusStat.maxHealth.GetValue(), _nexusStat.level, _nexusStat.levelupIncreaseValue);
        _nexusStat.level++;

        //nexusBase.HealthCompo.SetHealth(nexusBase.NexusStat);
        _nexusStat.upgradePrice *= 2; // <-�̰� �ӽ�
        WorkerManager.Instance.MaxWorkerCount++; //�̰͵� �ӽü���
        SoundManager.Play2DSound(SoundName.LevelUp); //�̰͵� �ӽ�
        
        NexusManager.Instance.UpdateNexusInfoData();

        if (TutorialManager.Instance.CurTutoQuestIdx == 3) //�ϴ� ����Ʈ
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
