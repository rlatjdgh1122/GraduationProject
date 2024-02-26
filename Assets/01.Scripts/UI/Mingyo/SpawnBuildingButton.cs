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

    public void SetUpButtonInfo(BuildingDatabaseSO buildingDatabaseSO, Button button, BuildingFactory buildingFactory, BuildingItemInfo buildinginfo, SpawnUI spawnUI, ConstructionStation constructionStation)
    {
        _buildingFactory = buildingFactory;
        _btn = button;

        transform.Find("Building_IMG").GetComponent<Image>().sprite = buildinginfo.UISprite;
        transform.Find("BuildingName_Text").GetComponent<TextMeshProUGUI>().SetText(buildinginfo.Name);
        transform.Find("InstaleedTime_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.InstalledTime} ��");

        transform.Find("Resource/NecessaryResource_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.NecessaryWokerCount}");
        transform.Find("Resource/NecessaryResource_IMG").GetComponent<Image>().sprite = buildinginfo.NecessaryResourceSprite;
        // HavingCount_Text �ؾߵ�


        installedTime = buildinginfo.InstalledTime; //�ϴ� ��Ÿ�� �޾ƿ��µ� �ǹ� ������ ������ �� ����� ������ �ϼ��Ǵ��� �߰� �ҵ�

        _btn.onClick.AddListener(() => SpawnBuildingEventHandler(buildinginfo.Prefab.GetComponent<BaseBuilding>(), buildingDatabaseSO));
        _btn.onClick.AddListener(() => spawnUI.OffUnitPanel());
        _btn.onClick.AddListener(() => spawnUI.OffBuildingPanel());
        _btn.onClick.AddListener(() => constructionStation.UpdateSpawnUIBool());

    }

    private void SpawnBuildingEventHandler(BaseBuilding spawnBuilding, BuildingDatabaseSO buildingDatabaseSO) //��ư �̺�Ʈ�� ������ �Լ�
    {
        bool cantSpawnBuilding = false;

        BuildingItemInfo building = buildingDatabaseSO.BuildingItems.Find(name => name.Name == spawnBuilding.gameObject.name); //��ȯ�� ����

        #region ���� ������
        if (WaveManager.Instance.IsBattlePhase)
        {
            cantSpawnBuilding = true;
            _buildingFactory.SetSpawnFailHudText("���� ������� ������ �� �����ϴ�");
        }
        #endregion

        #region �ڿ� ��
        Resource resource = ResourceManager.Instance.resourceStack.Find
            (icon => icon.resourceData.resourceIcon == building.NecessaryResourceSprite);


        if (resource.stackSize >= building.NecessaryResourceCount)
        {
            ResourceManager.Instance.RemoveResource(resource.resourceData, building.NecessaryResourceCount);
        }
        else
        {
            _buildingFactory.SetSpawnFailHudText("�ڿ��� �����մϴ�");
            cantSpawnBuilding = true;
        }
        #endregion

        #region �ϲ� �� ��

        if(WorkerManager.Instance.WorkerCount >= building.NecessaryResourceCount)
        {
           // WorkerManager.Instance.SendWorkers(building.NecessaryResourceCount, �ǹ�); ���� �ٲٸ� ����
        }
        else
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
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(_buildingFactory.SuccesHudText);

        _buildingFactory.SpawnBuildingHandler(spawnBuilding);
    }
}
