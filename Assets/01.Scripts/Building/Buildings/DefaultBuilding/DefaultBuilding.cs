using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private ConstructionStation _constructionStation;

    public override void Init()
    {
        isSpawnUIOn = false;
    }

    protected override void Start()
    {
        base.Start();

        Installed();
    }

    protected override void Awake()
    {
        base.Awake();

        _constructionStation = FindAnyObjectByType<ConstructionStation>().GetComponent<ConstructionStation>();

        SignalHub.OnBattlePhaseStartEvent += DisableAllUI;
    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && !InputReaderCompo.IsPointerOverUI())
        {
            SpawnButton();
        }
    }

    public void SpawnButton()
    {
        if (isSpawnUIOn)
        {
            UIManager.Instance.HidePanel("StorePanel");
        }
        else
        {
            UIManager.Instance.ShowPanel("StorePanel");
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
    }
    private void DisableAllUI()
    {
        if (isSpawnUIOn)
        {
            if (_defaultBuildingType == DefaultBuildingType.ConstructionStation)
            {
                StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
            }
            else
            {
                _penguinSpawnUI.OnDisableStorePanel();
            }
        }
        
    }

    protected override void Running()
    {
    }
}
