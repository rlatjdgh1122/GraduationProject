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
    private string buildingName;     
    private Texture2D _uiTexture;

    public void SetUpButtonInfo(Button button, BuildingFactory buildingFactory, BuildingItemInfo buildinginfo, SpawnUI spawnUI, PenguinSpawner penguinSpawner)
    {
        _buildingFactory = buildingFactory;
        _btn = button;

        transform.Find("Building_IMG").GetComponent<Image>().sprite = buildinginfo.UISprite;
        transform.Find("BuildingName_Text").GetComponent<TextMeshProUGUI>().SetText(buildinginfo.Name);

        installedTime = buildinginfo.installedTime; //일단 쿨타임 받아오는데 건물 누르면 앞으로 몇 페이즈가 지나야 완성되는지 뜨게 할듯

        _btn.onClick.AddListener(() => SpawnPenguinEventHandler(buildinginfo.Prefab.GetComponent<BaseBuilding>()));
        _btn.onClick.AddListener(() => spawnUI.OffUnitPanel());
        _btn.onClick.AddListener(() => spawnUI.OffBuildingPanel());
        _btn.onClick.AddListener(() => penguinSpawner.UpdateSpawnUIBool());
    }

    public void SpawnPenguinEventHandler(BaseBuilding spawnBuilding) //Inspector 버튼 이벤트에서 구독할 함수
    {
        if (WaveManager.Instance.IsPhase)
        {
            UIManager.Instance.InitializeWarningTextSequence();
            UIManager.Instance.WarningTextSequence.Prepend(_buildingFactory.SpawnFailHudText.DOFade(1f, 0.5f))
            .Join(_buildingFactory.SpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y, 0.5f))
            .Append(_buildingFactory.SpawnFailHudText.DOFade(0f, 0.5f))
            .Join(_buildingFactory.SpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y - 50f, 0.5f));

            return;
        }

        ButtonCooldown(spawnBuilding);
    }

    private void ButtonCooldown(BaseBuilding spawnBuilding) // 버튼 누르면 실행될 함수
    {
        _buildingFactory.SpawnBuildingHandler(spawnBuilding);
    }
}
