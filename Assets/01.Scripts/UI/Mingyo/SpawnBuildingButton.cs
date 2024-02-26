using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBuildingButton : MonoBehaviour
{
    private Button _btn;

    private BuildingFactory _buildingFactory; // 팩토리

    private float installedTime;

    public void SetUpButtonInfo(BuildingDatabaseSO buildingDatabaseSO, Button button, BuildingFactory buildingFactory, BuildingItemInfo buildinginfo, SpawnUI spawnUI, ConstructionStation constructionStation)
    {
        _buildingFactory = buildingFactory;
        _btn = button;

        transform.Find("Building_IMG").GetComponent<Image>().sprite = buildinginfo.UISprite;
        transform.Find("BuildingName_Text").GetComponent<TextMeshProUGUI>().SetText(buildinginfo.Name);
        transform.Find("InstaleedTime_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.InstalledTime} 턴");

        transform.Find("Resource/NecessaryResource_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.NecessaryWokerCount}");
        transform.Find("Resource/NecessaryResource_IMG").GetComponent<Image>().sprite = buildinginfo.NecessaryResourceSprite;
        // HavingCount_Text 해야됨


        installedTime = buildinginfo.InstalledTime; //일단 쿨타임 받아오는데 건물 누르면 앞으로 몇 페이즈가 지나야 완성되는지 뜨게 할듯

        _btn.onClick.AddListener(() => SpawnBuildingEventHandler(buildinginfo.Prefab.GetComponent<BaseBuilding>(), buildingDatabaseSO));
        _btn.onClick.AddListener(() => spawnUI.OffUnitPanel());
        _btn.onClick.AddListener(() => spawnUI.OffBuildingPanel());
        _btn.onClick.AddListener(() => constructionStation.UpdateSpawnUIBool());

    }

    private void SpawnBuildingEventHandler(BaseBuilding spawnBuilding, BuildingDatabaseSO buildingDatabaseSO) //버튼 이벤트에 구독된 함수
    {
        bool cantSpawnBuilding = false;

        BuildingItemInfo building = buildingDatabaseSO.BuildingItems.Find(name => name.Name == spawnBuilding.gameObject.name); //소환할 빌딩

        #region 전투 페이즈
        if (WaveManager.Instance.IsBattlePhase)
        {
            cantSpawnBuilding = true;
            _buildingFactory.SetSpawnFailHudText("전투 페이즈에는 생성할 수 없습니다");
        }
        #endregion

        #region 자원 비교
        Resource resource = ResourceManager.Instance.resourceStack.Find
            (icon => icon.resourceData.resourceIcon == building.NecessaryResourceSprite);


        if (resource.stackSize >= building.NecessaryResourceCount)
        {
            ResourceManager.Instance.RemoveResource(resource.resourceData, building.NecessaryResourceCount);
        }
        else
        {
            _buildingFactory.SetSpawnFailHudText("자원이 부족합니다");
            cantSpawnBuilding = true;
        }
        #endregion

        #region 일꾼 수 비교

        if(WorkerManager.Instance.WorkerCount >= building.NecessaryResourceCount)
        {
           // WorkerManager.Instance.SendWorkers(building.NecessaryResourceCount, 건물); 원석 바꾸면 수정
        }
        else
        {
            _buildingFactory.SetSpawnFailHudText("일꾼이 부족합니다");
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

    private void ButtonCooldown(BaseBuilding spawnBuilding) // 버튼 누르면 실행될 함수
    {
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(_buildingFactory.SuccesHudText);

        _buildingFactory.SpawnBuildingHandler(spawnBuilding);
    }
}
