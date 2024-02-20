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
    private TextMeshProUGUI _cancleInstallBuildingText, _buildingSpawnFailHudText;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private BuildingDatabaseSO _buildingDatabaseSO;
    private int selectedBuildingIDX = -1;

    [SerializeField]
    private InputReader _inputReader;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //Ä³½Ì¿ë µñ¼Å³Ê¸®

    private bool isInstalling;
    
    private Ground _previousGround;
    private BaseBuilding _curBuilding;

    private OutlineSelection _outlineSelection;

    private Ray _mousePointRay => Define.CamDefine.Cam.MainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

    private void Awake()
    {
        _outlineSelection = GameObject.FindAnyObjectByType<OutlineSelection>();
    }

    private void Start()
    {
        StopInstall();
    }

    public void SelectBuilding(BaseBuilding building)
    {
        _inputReader.OnLeftClickEvent += PlaceStructure;
        _inputReader.OnExitInstallEvent += StopInstall;

        _curBuilding = building;

        building.SetSelect();

        StartInstall(building.BuildingInfoCompo.ID);
    }

    private void StartInstall(int id)
    {
        selectedBuildingIDX = _buildingDatabaseSO.BuildingItems.FindIndex(building => building.ID == id);

        if (selectedBuildingIDX < 0)
        {
            Debug.LogError($"No id found {id}");
            return;
        }

        isInstalling = true;
        _cancleInstallBuildingText.enabled = true;
        //_outlineSelection.SetCursor(_buildingDatabaseSO.BuildingItems[selectedBuildingIDX].UITexture);
    }

    private void StopInstall()
    {
        selectedBuildingIDX = -1;

        _previousGround?.UpdateOutlineColor(GroundOutlineColorType.None);

        if (_curBuilding != null && !_curBuilding.IsInstalled)
        {
            _curBuilding.CancleInsall();
            PoolManager.Instance.Push(_curBuilding);
            _curBuilding = null;
        }

        _cancleInstallBuildingText.enabled = false;
        //_outlineSelection.SetDefaultCursor();

        _inputReader.OnLeftClickEvent -= PlaceStructure;
        _inputReader.OnExitInstallEvent -= StopInstall;
    }

    private void PlaceStructure()
    {
        //if (_inputReader.IsPointerOverUI())
        //{
        //    return;
        //}

        if (Physics.Raycast(_mousePointRay, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            if(_previousGround.IsInstalledBuilding)
            {
                UIManager.Instance.InitializeWarningTextSequence();
                UIManager.Instance.WarningTextSequence.Prepend(_buildingSpawnFailHudText.DOFade(1f, 0.5f))
                .Join(_buildingSpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y, 0.5f))
                .Append(_buildingSpawnFailHudText.DOFade(0f, 0.5f))
                .Join(_buildingSpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y - 50f, 0.5f));

                return;
            }

            _curBuilding?.Installed();
            _curBuilding?.transform.SetParent(_previousGround.transform);
            _previousGround?.InstallBuilding();
            StopInstall();
            isInstalling = false;
        }
    }

    private void Update()
    {
        if (selectedBuildingIDX < 0
         //|| _inputReader.IsPointerOverUI()
         || !isInstalling)
        { return; }


        if (Physics.Raycast(_mousePointRay, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // Ä³½Ì
            {
                Debug.Log("Add new Ground");
                _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
            }

            Ground _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

            if (_previousGround == null
             || _curGround != _previousGround)
            {
                _previousGround?.UpdateOutlineColor(GroundOutlineColorType.None);

                if (_curGround.IsInstalledBuilding)
                {
                    _curBuilding.transform.position = new Vector3(-10f, -10f, -10f);
                    _curGround.UpdateOutlineColor(GroundOutlineColorType.Red);
                }
                else
                {
                    _curGround.UpdateOutlineColor(GroundOutlineColorType.Green);

                    Vector3 buildingPos = new Vector3(_curGround.transform.position.x, 0f, _curGround.transform.position.z);
                    Vector3Int gridPosition = _curBuilding.BuildingInfoCompo.GridCompo.WorldToCell(buildingPos);
                    //_curBuilding.transform.position = _curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition); // ±×¸®µå·Î ÀÌµ¿
                    _curBuilding.transform.position = new Vector3(_curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition).x,
                                                                  2f,
                                                                  _curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition).z);
                }
            }

            _previousGround = _curGround;
        }
    }
}
