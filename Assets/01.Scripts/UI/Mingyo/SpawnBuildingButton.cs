using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBuildingButton : MonoBehaviour
{
    private Button _btn;

    [Tooltip("쿨타임")]
    [SerializeField] private float cooltime;

    [Tooltip("무엇을 생성할 것인지. Prefab을 넣어주면 된다.")]
    [SerializeField] private BaseBuilding spawnBuilding;

    private BuildingFactory _buildingFactory; // 팩토리

    protected virtual void Awake()
    {
        _buildingFactory = GameObject.Find("PenguinSpawner/BuildingFactory").GetComponent<BuildingFactory>();
        _btn = GetComponent<Button>();
    }

    public void SpawnPenguinEventHandler() //Inspector 버튼 이벤트에서 구독할 함수
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

        ButtonCooldown();
    }

    private void ButtonCooldown() // 버튼 누르면 실행될 함수
    {
        _buildingFactory.SpawnBuildingHandler(spawnBuilding);
    }
}
