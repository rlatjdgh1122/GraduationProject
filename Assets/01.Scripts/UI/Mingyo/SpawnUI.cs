using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUI : MonoBehaviour
{
    [SerializeField]
    RectTransform _unitSpawnUI, _buildingSpawnUI;

    private Vector3 _onSpawnUIVec, _offSpawnUIVec;

    private void Start()
    {
        _offSpawnUIVec = _buildingSpawnUI.position;
        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, 320, 0); //´ëÃæ°ª
    }

    public void OnUnitPanel()
    {
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_unitSpawnUI, _onSpawnUIVec, 0.7f, Ease.OutCubic));
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_buildingSpawnUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
    }

    public void OnBuildingPanel()
    {
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_buildingSpawnUI, _onSpawnUIVec, 0.7f, Ease.OutCubic));
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_unitSpawnUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
    }

    public void OffUnitPanel()
    {
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_unitSpawnUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
    }

    public void OffBuildingPanel()
    {
        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_buildingSpawnUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
    }
}
