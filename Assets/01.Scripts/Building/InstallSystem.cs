using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InstallSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _cancelInstallBuildingText, _buildingSpawnFailHudText;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private BuildingDatabaseSO _buildingDatabaseSO;
    private int selectedBuildingIDX = -1;

    [SerializeField]
    private InputReader _inputReader;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //캐싱용 딕셔너리

    private bool isInstalling;

    private Ground _previousGround;
    private BaseBuilding _curBuilding;

    private Ray _mousePointRay => Define.CamDefine.Cam.MainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

    private BuildingItemInfo _info;

    private NexusUIPresenter _nexusUIPresenter;

    private void Awake()
    {
        _nexusUIPresenter = FindObjectOfType<NexusUIPresenter>();
    }

    private void Start()
    {
        StopInstall();
    }

    public void SelectBuilding(BaseBuilding building, BuildingItemInfo info)
    {
        _info = info;

        _inputReader.OnLeftClickEvent += PlaceStructure;
        _inputReader.OnEscEvent += StopInstall;

        _inputReader.OnEBtnEvent += RightRotateBuilding;
        _inputReader.OnQBtnEvent += LeftRotateBuilding;

        _curBuilding = building;

        building.SetSelect(info);

        StartInstall(info);
    }

    private void RightRotateBuilding()
    {
        _curBuilding.transform.Rotate(0.0f, -90.0f, 0.0f);
    }

    private void LeftRotateBuilding()
    {
        _curBuilding.transform.Rotate(0.0f, 90.0f, 0.0f);
    }

    private void StartInstall(BuildingItemInfo info)
    {
        selectedBuildingIDX = info.ID;

        if (selectedBuildingIDX < 0)
        {
            Debug.LogError($"No id found {info.ID}");
            return;
        }

        isInstalling = true;
        _cancelInstallBuildingText.gameObject.SetActive(true);
    }

    private void StopInstall()
    {
        selectedBuildingIDX = -1;

        _curBuilding?.StopInstall();
        _previousGround?.UpdateOutlineColor(OutlineColorType.None);

        if (_curBuilding != null && !_curBuilding.IsInstalling)
        {
            PoolManager.Instance.Push(_curBuilding);
            _curBuilding = null;
        }

        _cancelInstallBuildingText.gameObject.SetActive(false);

        _inputReader.OnLeftClickEvent -= PlaceStructure;
        _inputReader.OnEscEvent -= StopInstall;
        _inputReader.OnEBtnEvent -= RightRotateBuilding;
        _inputReader.OnQBtnEvent -= LeftRotateBuilding;
    }

    private void PlaceStructure()
    {
        if (Physics.Raycast(_mousePointRay, Mathf.Infinity, _groundLayer))
        {
            if (_previousGround.IsInstalledBuilding &&
                _curBuilding.BuildingItemInfoCompo.BuildingTypeEnum != BuildingType.Trap)
            {
                UIManager.Instance.ShowWarningUI("이미 설치되어 있습니다");
                return;
            }

            foreach (var resource in _info.NecessaryResource)
            {
                ResourceManager.Instance.resourceDictionary.TryGetValue(resource.NecessaryResource.resourceData, out var saveResource);

                if (saveResource != null && saveResource.stackSize >= resource.NecessaryResourceCount)
                {
                    ResourceManager.Instance.RemoveResource(resource.NecessaryResource.resourceData, resource.NecessaryResourceCount);
                }
            }


            _info.CurrentInstallCount++;
            _nexusUIPresenter.UpdateRecieverUI();

            UIManager.Instance.ShowWarningUI("설치 완료!");

            _curBuilding?.Installed();
            _curBuilding?.transform.SetParent(_previousGround.transform);

            _previousGround?.InstallBuilding();
            StopInstall();

            if (TutorialManager.Instance.CurTutoQuestIdx == 2 ||
                TutorialManager.Instance.CurTutoQuestIdx == 3) //일단 퀘스트
            {
                TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
            }
            isInstalling = false;
        }
    }

    private void Update()
    {
        if (selectedBuildingIDX < 0
         //|| _inputReader.IsPointerOverUI()
         || !isInstalling)
        { return; }


        Installing();
    }

    private void Installing()
    {
        if (Physics.Raycast(_mousePointRay, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            int hashCode = hit.transform.gameObject.GetHashCode();
            if (!_groundDic.ContainsKey(hashCode)) // 캐싱
            {
                _groundDic.Add(hashCode, hit.transform.parent.GetComponent<Ground>());
            }

            Ground curGround = _groundDic[hashCode];

            if (_previousGround == null ||
                curGround != _previousGround)
            {
                _previousGround?.UpdateOutlineColor(OutlineColorType.None);
            }

            if (_curBuilding.BuildingItemInfoCompo.BuildingTypeEnum ==
                BuildingType.Trap)
            {
                MoveTrap(hit.point);
                _curBuilding.ChangeToTransparencyMat(OutlineColorType.Green);
            }
            else
            {
                MoveSelectBuilding(curGround);
                UpdateGroundColor(curGround);
            }

            _previousGround = curGround;
        }
    }

    private void MoveSelectBuilding(Ground curGround)
    {
        Vector3 buildingPos = new Vector3(curGround.transform.position.x, 0f, curGround.transform.position.z);
        Vector3Int gridPosition = _curBuilding.BuildingInfoCompo.GridCompo.WorldToCell(buildingPos);
        //_curBuilding.transform.position = _curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition); // 그리드로 이동
        _curBuilding.transform.position = new Vector3(_curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition).x,
                                                      1.5f,
                                                      _curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition).z);
    }

    private void MoveTrap(Vector3 pos)
    {
        _curBuilding.transform.position = pos;
    }

    private void UpdateGroundColor(Ground curGround)
    {
        if (curGround.IsInstalledBuilding)
        {
            curGround.UpdateOutlineColor(OutlineColorType.Red);
            _curBuilding.ChangeToTransparencyMat(OutlineColorType.Red);
        }
        else
        {
            curGround.UpdateOutlineColor(OutlineColorType.Green);   
            _curBuilding.ChangeToTransparencyMat(OutlineColorType.Green);

        }
    }
}
