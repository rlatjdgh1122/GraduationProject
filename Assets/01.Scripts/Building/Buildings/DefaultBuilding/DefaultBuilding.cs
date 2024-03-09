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

    [SerializeField] private RectTransform _penguinSpawnUI;
    [SerializeField] private RectTransform _constructionStationUI;

    [SerializeField] private float onSpawnUIYPosValue = 320;

    private bool isSpawnUIOn;

    Vector3 _onSpawnUIVec;
    Vector3 _offSpawnUIVec;

    [SerializeField] private Transform _btnTrm;

    protected Outline _outline;

    private ConstructionStation _constructionStation;
    private PenguinSpawner _penguinSpawner;


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

        _penguinSpawner = FindAnyObjectByType<PenguinSpawner>().GetComponent<PenguinSpawner>();
        _constructionStation = FindAnyObjectByType<ConstructionStation>().GetComponent<ConstructionStation>();

        WaveManager.Instance.OnBattlePhaseStartEvent += DisableAllUI;
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
        Vector3 targetVec = isSpawnUIOn ? _offSpawnUIVec : _onSpawnUIVec;
        UpdateSpawnUIBool();

        if (_defaultBuildingType == DefaultBuildingType.ConstructionStation)
        {
            StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, targetVec, 0.7f, Ease.OutCubic));
            StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_penguinSpawnUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));

            if (_penguinSpawner.isSpawnUIOn)
            {
                _penguinSpawner.UpdateSpawnUIBool();
            }
        }
        else
        {
            StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_penguinSpawnUI, targetVec, 0.7f, Ease.OutCubic));
            StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));

            if (_constructionStation.isSpawnUIOn)
            {
                _constructionStation.UpdateSpawnUIBool();
            }
        }
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
                StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_penguinSpawnUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
                _outline.enabled = false;
            }
        }
        
    }

    protected override void Running()
    {
    }
}
