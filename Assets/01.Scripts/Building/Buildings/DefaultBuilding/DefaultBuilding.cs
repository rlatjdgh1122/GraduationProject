using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultBuilding : BaseBuilding
{
    [SerializeField] private LayerMask _defaultBuildingLayer;

    [SerializeField] DefaultBuildingType _defaultBuildingType;

    //private PenguinStoreUI _penguinSpawnUI;
    //[SerializeField] private RectTransform _constructionStationUI;

    //[SerializeField] private float onSpawnUIYPosValue = 320;

    private bool isSpawnUIOn;

    //Vector3 _onSpawnUIVec;
    //Vector3 _offSpawnUIVec;

    private Outline _outline;

    private ConstructionStation _constructionStation;

    private bool isFirst = true;

    public override void Init()
    {
        isSpawnUIOn = false;
    }

    protected override void Awake()
    {
        base.Awake();

        _constructionStation = FindAnyObjectByType<ConstructionStation>().GetComponent<ConstructionStation>();
        _outline = GetComponent<Outline>();
        //_penguinSpawnUI = FindObjectOfType<PenguinStoreUI>();

        //SignalHub.OnBattlePhaseStartEvent += DisableAllUI;
        SetInstalled();
        InstalledGround()?.InstallBuilding();
    }

    private void OnMouseDown()
    {
        if(WaveManager.Instance.CurrentWaveCount == 3)
        {
            UIManager.Instance.ShowWarningUI("튜토리얼이 진행 중입니다");
            return;
        }

        if (!WaveManager.Instance.IsBattlePhase/* && !InputReaderCompo.IsPointerOverUI()*/
            && !UIManager.Instance.GifController.CanShow
            && !LegionInventoryManager.Instance.CanUI
            && !NexusManager.Instance.CanClick
            && !TutorialManager.Instance.ShowTutorialQuest)
        {
            if (isFirst)
            {
                UIManager.Instance.GifController.ShowGif(GifType.PenguinBuy);
                isFirst = false;
            }
            SpawnButton();
        }
    }

    public void SpawnButton()
    {
        if (isSpawnUIOn)
        {
            UIManager.Instance.HidePanel("StorePanel");
            ChangeSpawnUIBool(false);
        }
        else
        {
            UIManager.Instance.ShowPanel("StorePanel");
            ChangeSpawnUIBool(true);
            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }

        //StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));

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

        BuildingOutline();
    }

    public void ChangeSpawnUIBool(bool value)
    {
        isSpawnUIOn = value;

        BuildingOutline();
    }

    private void BuildingOutline()
    {
        _outline.enabled = isSpawnUIOn;
    }

    //private void DisableAllUI()
    //{
    //    if (isSpawnUIOn)
    //    {
    //        if (_defaultBuildingType == DefaultBuildingType.ConstructionStation)
    //        {
    //            StartCoroutine(UIManager.Instance.UIMoveDotCoroutine(_constructionStationUI, _offSpawnUIVec, 0.7f, Ease.OutCubic));
    //        }
    //        else
    //        {
    //            //_penguinSpawnUI.OnDisableStorePanel();
    //        }
    //    }
        
    //}

    protected override void Running()
    {
    }
}
