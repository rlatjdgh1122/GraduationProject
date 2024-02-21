using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Outline))]
public class DefaultBuilding : BaseBuilding
{
    [SerializeField] DefaultBuildingType _defaultBuildingType;

    [SerializeField] private RectTransform _penguinSpawnUI;
    [SerializeField] private RectTransform _constructionStationUI;

    [SerializeField] private float onSpawnUIYPosValue = 320;
    [SerializeField] private LayerMask _buildingLayer;

    private bool isSpawnUIOn;

    Vector3 _onSpawnUIVec;
    Vector3 _offSpawnUIVec;

    [SerializeField] private Transform _btnTrm;

    private Outline _outline;

    protected virtual void Start()
    {
        Installed();
        _offSpawnUIVec = _penguinSpawnUI.position;
        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
    }

    protected override void Awake()
    {
        base.Awake();
        _outline = GetComponent<Outline>();
    }

    protected override void Running()
    {
        if (Input.GetMouseButtonDown(0) && !WaveManager.Instance.IsBattlePhase)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _buildingLayer))
            {
                SpawnButton();
            }
        }
    }

    public void SpawnButton()
    {
        Vector3 targetVec = isSpawnUIOn ? _offSpawnUIVec : _onSpawnUIVec;

        if (_defaultBuildingType == DefaultBuildingType.ConstructionStation)
        {
            UIManager.Instance.UIMoveDot(_constructionStationUI, targetVec, 0.7f, Ease.OutCubic);
            UIManager.Instance.UIMoveDot(_penguinSpawnUI, _offSpawnUIVec, 0.7f, Ease.OutCubic);
        }
        else
        {
            UIManager.Instance.UIMoveDot(_penguinSpawnUI, targetVec, 0.7f, Ease.OutCubic);
            UIManager.Instance.UIMoveDot(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic);
        }
        
        UpdateSpawnUIBool();
    }

    public virtual void UpdateSpawnUIBool()
    {
        isSpawnUIOn = isSpawnUIOn ? false : true;
        _outline.enabled = isSpawnUIOn;
    }
}
