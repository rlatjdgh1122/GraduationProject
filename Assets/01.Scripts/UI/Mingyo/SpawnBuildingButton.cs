using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBuildingButton : MonoBehaviour
{
    private Button _btn;

    [Tooltip("��Ÿ��")]
    [SerializeField] private float cooltime; //�ϴ��� �ð����� �ص�

    private BuildingFactory _buildingFactory; // ���丮

    protected virtual void Awake()
    {
        _buildingFactory = GameObject.Find("BuildingFactory").GetComponent<BuildingFactory>();
        _btn = GetComponent<Button>();
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
