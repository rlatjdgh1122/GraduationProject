using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBuildingButton : MonoBehaviour
{
    private Button _btn;
    private BuildingFactory _buildingFactory; // ���丮

    private float installedTime;

    public void SetUpButtonInfo(Button button, BuildingFactory buildingFactory, BuildingItemInfo buildinginfo)
    {
        _buildingFactory = buildingFactory;
        _btn = button;

        //transform.Find("Building_IMG").GetComponent<Image>().sprite = buildinginfo.UISprite;
        //transform.Find("BuildingName_Text").GetComponent<TextMeshProUGUI>().SetText(buildinginfo.Name);
        //transform.Find("InstaleedTime_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.InstalledTime} ��");

        //transform.Find("Resource/NecessaryResource_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.NecessaryWokerCount}");
        //transform.Find("Resource/NecessaryResource_IMG").GetComponent<Image>().sprite = buildinginfo.NecessaryResource.resourceData.resourceIcon;
        // HavingCount_Text �ؾߵ�


        installedTime = buildinginfo.InstalledTime;

        //_btn.onClick.AddListener(() => spawnUI.OffUnitPanel());
        //_btn.onClick.AddListener(() => spawnUI.OffBuildingPanel());
        //_btn.onClick.AddListener(() => constructionStation.UpdateSpawnUIBool());
    }

    public void SpawnBuildingEventHandler(BaseBuilding spawnBuilding, BuildingItemInfo buildinginfo) //��ư �̺�Ʈ�� ������ �Լ�
    {
        bool cantSpawnBuilding = false;

        #region ���� ������
        if (WaveManager.Instance.IsBattlePhase)
        {
            cantSpawnBuilding = true;
            _buildingFactory.SetSpawnFailHudText("���� ������� ������ �� �����ϴ�");
        }
        #endregion

        #region �ڿ� ��

        //try
        //{
        //    Resource resource = ResourceManager.Instance.resourceStack.Find
        //    (icon => icon.resourceData.resourceIcon == buildinginfo.NecessaryResource.resourceData.resourceIcon);

        //    if (resource.stackSize >= buildinginfo.NecessaryResourceCount)
        //    {
        //        ResourceManager.Instance.RemoveResource(resource.resourceData, buildinginfo.NecessaryResourceCount);
        //    }
        //    else
        //    {
        //        _buildingFactory.SetSpawnFailHudText("�ڿ��� �����մϴ�");
        //        cantSpawnBuilding = true;
        //    }
        //}
        //catch
        //{
        //    _buildingFactory.SetSpawnFailHudText("�ڿ��� �����մϴ�");
        //    cantSpawnBuilding = true;
        //}

        #endregion

        #region �ϲ� �� ��

        if (!(WorkerManager.Instance.WorkerCount >= buildinginfo.NecessaryResourceCount))
        {
            _buildingFactory.SetSpawnFailHudText("�ϲ��� �����մϴ�");
            cantSpawnBuilding = true;
        }

        #endregion


        if (cantSpawnBuilding)
        {
            UIManager.Instance.InitializHudTextSequence();
            UIManager.Instance.SpawnHudText(_buildingFactory.FailHudText);

            return;
        }

        ButtonCooldown(spawnBuilding);
    }

    private void ButtonCooldown(BaseBuilding spawnBuilding) // ��ư ������ ����� �Լ�
    {
        Debug.Log("��");
        UIManager.Instance.HidePanel("NexusUI");
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(_buildingFactory.SuccesHudText);

        _buildingFactory.SpawnBuildingHandler(spawnBuilding);
    }
}
