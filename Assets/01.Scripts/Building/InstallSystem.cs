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
    private TextMeshProUGUI _cancelInstallBuildingText; // 설치 취소 안내 텍스트

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

    private NexusUIPresenter _nexusUIPresenter;

    private void Awake()
    {
        _nexusUIPresenter = FindObjectOfType<NexusUIPresenter>();
    }

    private void Start()
    {
        StopInstall();
    }

    public void SelectBuilding(BaseBuilding building, BuildingItemInfo info) // UI에서 건물 누르면 실핼되는 함수
    {
        selectedBuildingIDX = info.ID;

        if (selectedBuildingIDX < 0) // 만약 id를 찾을 수 없다면
        {
            Debug.LogError($"No id found {info.ID}");
            return;
        }

        StartInstall(building, info);
    }

    private void RightRotateBuilding()
    {
        _curBuilding.transform.Rotate(0.0f, -90.0f, 0.0f);
    }

    private void LeftRotateBuilding()
    {
        _curBuilding.transform.Rotate(0.0f, 90.0f, 0.0f);
    }

    private void StartInstall(BaseBuilding building, BuildingItemInfo info)
    {
        // 인풋 이벤트들 구독하고
        _inputReader.OnLeftClickEvent += PlaceStructure;
        _inputReader.OnEscEvent += StopInstall;

        _inputReader.OnEBtnEvent += RightRotateBuilding;
        _inputReader.OnQBtnEvent += LeftRotateBuilding;

        // 현재 건물 설정
        _curBuilding = building;

        // 건물에 info 설정
        _curBuilding.SetSelect(info);

        isInstalling = true;
        _cancelInstallBuildingText.gameObject.SetActive(true); // 설치 취소 안내 텍스트 켜줌
    }

    private void StopInstall() // 설치가 끝나거나 x를 눌러 취소하면 실행되는 함수
    {
        selectedBuildingIDX = -1; // idx 바꿔주고

        _curBuilding?.StopInstall();                                // 땅과 건물에 설치취소 처리 
        _previousGround?.UpdateOutlineColor(OutlineColorType.None); // 땅과 건물에 설치취소 처리 

        if (_curBuilding != null && !_curBuilding.IsInstalling) // esc를 눌러서 설치를 취소 했으면
        {
            PoolManager.Instance.Push(_curBuilding);
            _curBuilding = null;
        }

        _cancelInstallBuildingText.gameObject.SetActive(false); // 설치 취소 안내 텍스트 꺼줌


        // 인풋 이벤트들 구독 해제
        _inputReader.OnLeftClickEvent -= PlaceStructure;
        _inputReader.OnEscEvent -= StopInstall;
        _inputReader.OnEBtnEvent -= RightRotateBuilding;
        _inputReader.OnQBtnEvent -= LeftRotateBuilding;
    }

    private void PlaceStructure() // 건물을 선택하고 클릭을 눌러 설치 하려고 할때
    {
        if (Physics.Raycast(_mousePointRay, Mathf.Infinity, _groundLayer))
        {
            if (_previousGround.IsInstalledBuilding &&
                _curBuilding.BuildingItemInfoCompo.BuildingTypeEnum != BuildingType.Trap) // 이미 건물이 설치 되어 있다면
            {
                UIManager.Instance.ShowWarningUI("이미 설치되어 있습니다");
                return;
            }

            foreach (var resource in _curBuilding.BuildingItemInfoCompo.NecessaryResource) // 필요한 자원들
            {
                ResourceManager.Instance.resourceDictionary.TryGetValue(resource.NecessaryResource.resourceData, out var saveResource);

                if (saveResource != null && saveResource.stackSize >= resource.NecessaryResourceCount)
                {
                    ResourceManager.Instance.RemoveResource(resource.NecessaryResource.resourceData, resource.NecessaryResourceCount);
                }
            }


            _curBuilding.BuildingItemInfoCompo.CurrentInstallCount++;
            _nexusUIPresenter.UpdateRecieverUI();

            UIManager.Instance.ShowWarningUI("설치 완료!");

            _curBuilding?.Installed();                                    // 건물에 설치 처리 하고 위치 설정
            _curBuilding?.transform.SetParent(_previousGround.transform); // 건물에 설치 처리 하고 위치 설정

            _previousGround?.InstallBuilding(); // 땅에 설치 처리
            StopInstall(); // 설치 중 상태를 벗어나기 위한 StopInstall

            //if (TutorialManager.Instance.CurTutoQuestIdx == 2 ||
            //    TutorialManager.Instance.CurTutoQuestIdx == 3) //일단 퀘스트
            //{
            //    TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
            //}
            isInstalling = false;
        }
    }

    private void Update()
    {
        if (selectedBuildingIDX < 0
         //|| _inputReader.IsPointerOverUI()
         || !isInstalling)
        { return; } // 설치 중 상태가 아니면 return


        Installing();
    }

    private void Installing()
    {
        if (Physics.Raycast(_mousePointRay, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            int hashCode = hit.transform.gameObject.GetHashCode();
            if (!_groundDic.ContainsKey(hashCode)) // Ground 캐싱
            {
                _groundDic.Add(hashCode, hit.transform.parent.GetComponent<Ground>());
            }

            Ground curGround = _groundDic[hashCode];

            if (_previousGround == null ||
                curGround != _previousGround)
            {
                UpdateGroundColor(curGround);
            }

            if (_curBuilding.BuildingItemInfoCompo.BuildingTypeEnum == // 함정 건물이면 그냥 마우스 따라가게 
                BuildingType.Trap)
            {
                MoveTrap(hit.point);
                _curBuilding.ChangeToTransparencyMat(OutlineColorType.Green); // 함정 건물이면 무조건 땅에 건물이 설치되어 있어도 설치 되야 하니까
            }
            else
            {
                MoveSelectBuilding(curGround);
            }

            _previousGround = curGround;
        }
    }

    private void MoveSelectBuilding(Ground curGround) // 그리드로 움직이게
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

    private void UpdateGroundColor(Ground curGround) // 땅의 Outline 색을 바꿔주는 함수
    {
        _previousGround?.UpdateOutlineColor(OutlineColorType.None);

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
