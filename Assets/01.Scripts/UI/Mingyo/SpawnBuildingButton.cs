using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBuildingButton : MonoBehaviour
{
    private Button _btn;

    [Tooltip("쿨타임")]
    [SerializeField] private float cooltime; //일단은 시간으로 해둠

    private BuildingFactory _buildingFactory; // 팩토리

    protected virtual void Awake()
    {
        _buildingFactory = GameObject.Find("BuildingFactory").GetComponent<BuildingFactory>();
        _btn = GetComponent<Button>();
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
