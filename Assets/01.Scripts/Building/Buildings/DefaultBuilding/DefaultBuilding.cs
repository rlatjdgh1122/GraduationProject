using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline))]
public class DefaultBuilding : BaseBuilding
{
    [SerializeField] private LayerMask _defaultBuildingLayer;

    [SerializeField] DefaultBuildingType _defaultBuildingType;

    [SerializeField] private PenguinStoreUI _penguinSpawnUI;
    [SerializeField] private RectTransform _constructionStationUI;

    [SerializeField] private float onSpawnUIYPosValue = 320;

    private bool isSpawnUIOn;

    Vector3 _onSpawnUIVec;
    Vector3 _offSpawnUIVec;

    [SerializeField] private Transform _btnTrm;

    protected Outline _outline;

    private ConstructionStation _constructionStation;

    protected virtual void Start()
    {
        Installed();

        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
    }

    protected override void Awake()
    {
        base.Awake();
        _outline = GetComponent<Outline>();

        _constructionStation = FindAnyObjectByType<ConstructionStation>().GetComponent<ConstructionStation>();

        SignalHub.OnBattlePhaseStartEvent += DisableAllUI;
    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && !InputReaderCompo.IsPointerOverUI() && IsInstalled)
        {
            SpawnButton();
        }
    }

    public void SpawnButton()
    {
        if (isSpawnUIOn)
        {
            _penguinSpawnUI.OnDisableStorePanel();
        }
        else
        {
            _penguinSpawnUI.OnEnableStorePanel();
        }

        UpdateSpawnUIBool();

        StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));

        if (_constructionStation.isSpawnUIOn)
        {
            _constructionStation.UpdateSpawnUIBool();
        }

        //if (_defaultBuildingType == DefaultBuildingType.ConstructionStation)
        //{
        //    StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, targetVec, 0.7f, Ease.OutCubic));

        //    if (_penguinSpawner.isSpawnUIOn)
        //    {
        //        _penguinSpawner.UpdateSpawnUIBool();
        //    }
        //}
        //else
        //{

        //    StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));

        //    if (_constructionStation.isSpawnUIOn)
        //    {
        //        _constructionStation.UpdateSpawnUIBool();
        //    }
        //}
    }

    public virtual void UpdateSpawnUIBool()
    {
        isSpawnUIOn = isSpawnUIOn ? false : true;
        _outline.enabled = isSpawnUIOn;
    }
    private void DisableAllUI()
    {
        if (isSpawnUIOn)
        {
            if (_defaultBuildingType == DefaultBuildingType.ConstructionStation)
            {
                StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
                _outline.enabled = false;
            }
            else
            {
                _penguinSpawnUI.OnDisableStorePanel();
                _outline.enabled = false;
            }
        }
        
    }

    protected override void Running()
    {
    }
}
