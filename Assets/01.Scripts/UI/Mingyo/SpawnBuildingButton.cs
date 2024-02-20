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
    private string buildingName;     
    private Texture2D _uiTexture;

    public void SetUpButtonInfo(Button button, BuildingFactory buildingFactory, BuildingItemInfo buildinginfo, SpawnUI spawnUI, PenguinSpawner penguinSpawner)
    {
        _buildingFactory = buildingFactory;
        _btn = button;

        transform.Find("Building_IMG").GetComponent<Image>().sprite = buildinginfo.UISprite;
        transform.Find("BuildingName_Text").GetComponent<TextMeshProUGUI>().SetText(buildinginfo.Name);

        installedTime = buildinginfo.installedTime; //�ϴ� ��Ÿ�� �޾ƿ��µ� �ǹ� ������ ������ �� ����� ������ �ϼ��Ǵ��� �߰� �ҵ�

        _btn.onClick.AddListener(() => SpawnPenguinEventHandler(buildinginfo.Prefab.GetComponent<BaseBuilding>()));
        _btn.onClick.AddListener(() => spawnUI.OffUnitPanel());
        _btn.onClick.AddListener(() => spawnUI.OffBuildingPanel());
        _btn.onClick.AddListener(() => penguinSpawner.UpdateSpawnUIBool());
    }

    public void SpawnPenguinEventHandler(BaseBuilding spawnBuilding) //Inspector ��ư �̺�Ʈ���� ������ �Լ�
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

    private void ButtonCooldown(BaseBuilding spawnBuilding) // ��ư ������ ����� �Լ�
    {
        _buildingFactory.SpawnBuildingHandler(spawnBuilding);
    }
}
