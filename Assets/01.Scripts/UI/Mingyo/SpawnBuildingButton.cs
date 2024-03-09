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

    public void SetUpButtonInfo(Button button, BuildingFactory buildingFactory, BuildingItemInfo buildinginfo)
    {
        _buildingFactory = buildingFactory;
        _btn = button;

        //transform.Find("Building_IMG").GetComponent<Image>().sprite = buildinginfo.UISprite;
        //transform.Find("BuildingName_Text").GetComponent<TextMeshProUGUI>().SetText(buildinginfo.Name);
        //transform.Find("InstaleedTime_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.InstalledTime} 턴");

        //transform.Find("Resource/NecessaryResource_Text").GetComponent<TextMeshProUGUI>().SetText($"{buildinginfo.NecessaryWokerCount}");
        //transform.Find("Resource/NecessaryResource_IMG").GetComponent<Image>().sprite = buildinginfo.NecessaryResource.resourceData.resourceIcon;
        // HavingCount_Text 해야됨


        installedTime = buildinginfo.InstalledTime;

        //_btn.onClick.AddListener(() => spawnUI.OffUnitPanel());
        //_btn.onClick.AddListener(() => spawnUI.OffBuildingPanel());
        //_btn.onClick.AddListener(() => constructionStation.UpdateSpawnUIBool());
    }

    public void SpawnBuildingEventHandler(BaseBuilding spawnBuilding, BuildingItemInfo buildinginfo) //버튼 이벤트에 구독된 함수
    {
        bool cantSpawnBuilding = false;

        #region 전투 페이즈
        if (WaveManager.Instance.IsBattlePhase)
        {
            cantSpawnBuilding = true;
            _buildingFactory.SetSpawnFailHudText("전투 페이즈에는 생성할 수 없습니다");
        }
        #endregion

        #region 자원 비교

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
        //        _buildingFactory.SetSpawnFailHudText("자원이 부족합니다");
        //        cantSpawnBuilding = true;
        //    }
        //}
        //catch
        //{
        //    _buildingFactory.SetSpawnFailHudText("자원이 부족합니다");
        //    cantSpawnBuilding = true;
        //}

        #endregion

        #region 일꾼 수 비교

        if (!(WorkerManager.Instance.WorkerCount >= buildinginfo.NecessaryResourceCount))
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
        Debug.Log("ㅇ");
        UIManager.Instance.HidePanel("NexusUI");
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(_buildingFactory.SuccesHudText);

        _buildingFactory.SpawnBuildingHandler(spawnBuilding);
    }
}
