using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InstallSysytem : MonoBehaviour
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

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //캐싱용 딕셔너리

    private bool isInstalling;
    
    private Ground _previousGround;

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
        _outlineSelection.SetCursor(_buildingDatabaseSO.BuildingItems[selectedBuildingIDX].UITexture);
    }

    private void StopInstall()
    {
        selectedBuildingIDX = -1;

        _previousGround?.UpdateOutlineColor(GroundOutlineColorType.None);

        _cancleInstallBuildingText.enabled = false;
        _outlineSelection.SetDefaultCursor();

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
            

            BaseBuilding curSelectedBuilding = PoolManager.Instance.Pop(_buildingDatabaseSO.BuildingItems[selectedBuildingIDX].Name) as BaseBuilding;

            Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y + 1, hit.transform.position.z);
            Vector3Int gridPosition = curSelectedBuilding.BuildingInfoCompo.GridCompo.WorldToCell(hitPos);
            curSelectedBuilding.transform.position = curSelectedBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition); // 그리드로 이동

            curSelectedBuilding?.Installed();
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
            if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // 캐싱
            {
                _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
            }

            Ground _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

            if (_previousGround == null
             || _curGround != _previousGround)
            {
                _previousGround?.UpdateOutlineColor(GroundOutlineColorType.None);

                if (_curGround.IsInstalledBuilding)
                {
                    _curGround.UpdateOutlineColor(GroundOutlineColorType.Red);
                }
                else
                {
                    _curGround.UpdateOutlineColor(GroundOutlineColorType.Green);
                }
            }

            _previousGround = _curGround;
        }
    }

    //private IEnumerator BuildingFollowMousePosition(BaseBuilding curSelectedBuilding)
    //{
    //    while (true)
    //    {
    //        if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
    //                                            out var hit, Mathf.Infinity, _groundLayer))
    //        {
    //            if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // 캐싱
    //            {
    //                _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
    //            }

    //            Ground _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

    //            // 만약 미리 설치되어 있으면 
    //            if (_curGround.IsInstalledBuilding && !_curGround.IsRedMT)
    //            {
    //                Debug.Log("This ground already installed");
    //                _curGround.UpdateOutlineColor(false); // 설치 불가능하다고 나타냄
    //                yield return null;
    //            }
    //            else
    //            {
    //                if (!_curGround.IsGreenMT) { _curGround.UpdateOutlineColor(true); } // 설치 가능하다고 나타냄

    //                Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
    //                Vector3Int gridPosition = curSelectedBuilding.BuildingInfoCompo.GridCompo.WorldToCell(hitPos);
    //                curSelectedBuilding.transform.position = curSelectedBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition); // 그리드로 이동

    //                if (Input.GetMouseButtonDown(0)) // 한번 더 누르면 설치
    //                {
    //                    curSelectedBuilding.transform.SetParent(hit.transform);
    //                    curSelectedBuilding.Installed(); // 건물에 설치 처리
    //                    _curGround.InstallBuilding(); // 땅에 설치 처리

    //                    yield break; // 코루틴 종료
    //                }
    //            }
    //        }

    //        yield return null;
    //    }
    //}
}
