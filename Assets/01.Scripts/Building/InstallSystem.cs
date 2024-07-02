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
    private TextMeshProUGUI _cancelInstallBuildingText; // ��ġ ��� �ȳ� �ؽ�Ʈ

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private BuildingDatabaseSO _buildingDatabaseSO;
    private int selectedBuildingIDX = -1;

    [SerializeField]
    private InputReader _inputReader;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //ĳ�̿� ��ųʸ�

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

    public void SelectBuilding(BaseBuilding building, BuildingItemInfo info) // UI���� �ǹ� ������ ���۵Ǵ� �Լ�
    {
        selectedBuildingIDX = info.ID;

        if (selectedBuildingIDX < 0) // ���� id�� ã�� �� ���ٸ�
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
        // ��ǲ �̺�Ʈ�� �����ϰ�
        _inputReader.OnLeftClickEvent += PlaceStructure;
        _inputReader.OnEscEvent += StopInstall;

        _inputReader.OnEBtnEvent += RightRotateBuilding;
        _inputReader.OnQBtnEvent += LeftRotateBuilding;

        // ���� �ǹ� ����
        _curBuilding = building;

        // �ǹ��� info ����
        _curBuilding.SetSelect(info);

        isInstalling = true;
        _cancelInstallBuildingText.gameObject.SetActive(true); // ��ġ ��� �ȳ� �ؽ�Ʈ ����
    }

    private void StopInstall() // ��ġ�� �����ų� x�� ���� ����ϸ� ����Ǵ� �Լ�
    {
        selectedBuildingIDX = -1; // idx �ٲ��ְ�

        _curBuilding?.StopInstall();                                // ���� �ǹ��� ��ġ��� ó�� 
        _previousGround?.UpdateOutlineColor(OutlineColorType.None); // ���� �ǹ��� ��ġ��� ó�� 

        if (_curBuilding != null && !_curBuilding.IsInstalling) // esc�� ������ ��ġ�� ��� ������
        {
            PoolManager.Instance.Push(_curBuilding);
            _curBuilding = null;
        }

        _cancelInstallBuildingText.gameObject.SetActive(false); // ��ġ ��� �ȳ� �ؽ�Ʈ ����


        // ��ǲ �̺�Ʈ�� ���� ����
        _inputReader.OnLeftClickEvent -= PlaceStructure;
        _inputReader.OnEscEvent -= StopInstall;
        _inputReader.OnEBtnEvent -= RightRotateBuilding;
        _inputReader.OnQBtnEvent -= LeftRotateBuilding;
    }

    private void PlaceStructure() // �ǹ��� �����ϰ� Ŭ���� ���� ��ġ �Ϸ��� �Ҷ�
    {
        if (Physics.Raycast(_mousePointRay, Mathf.Infinity, _groundLayer))
        {
            if (_previousGround.IsInstalledBuilding &&
                _curBuilding.BuildingItemInfoCompo.BuildingTypeEnum != BuildingType.Trap) // �̹� �ǹ��� ��ġ �Ǿ� �ִٸ�
            {
                UIManager.Instance.ShowWarningUI("�̹� ��ġ�Ǿ� �ֽ��ϴ�");
                return;
            }

            foreach (var resource in _curBuilding.BuildingItemInfoCompo.NecessaryResource) // �ʿ��� �ڿ���
            {
                ResourceManager.Instance.resourceDictionary.TryGetValue(resource.NecessaryResource.resourceData, out var saveResource);

                if (saveResource != null && saveResource.stackSize >= resource.NecessaryResourceCount)
                {
                    ResourceManager.Instance.RemoveResource(resource.NecessaryResource.resourceData, resource.NecessaryResourceCount);
                }
            }


            _curBuilding.BuildingItemInfoCompo.CurrentInstallCount++;
            _nexusUIPresenter.UpdateRecieverUI();

            UIManager.Instance.ShowWarningUI("��ġ �Ϸ�!");

            _curBuilding?.Installed();                                    // �ǹ��� ��ġ ó�� �ϰ� ��ġ ����
            _curBuilding?.transform.SetParent(_previousGround.transform); // �ǹ��� ��ġ ó�� �ϰ� ��ġ ����

            _previousGround?.InstallBuilding(); // ���� ��ġ ó��
            StopInstall(); // ��ġ �� ���¸� ����� ���� StopInstall

            //if (TutorialManager.Instance.CurTutoQuestIdx == 2 ||
            //    TutorialManager.Instance.CurTutoQuestIdx == 3) //�ϴ� ����Ʈ
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
        { return; } // ��ġ �� ���°� �ƴϸ� return


        Installing();
    }

    private void Installing()
    {
        if (Physics.Raycast(_mousePointRay, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            int hashCode = hit.transform.gameObject.GetHashCode();
            if (!_groundDic.ContainsKey(hashCode)) // Ground ĳ��
            {
                _groundDic.Add(hashCode, hit.transform.parent.GetComponent<Ground>());
            }

            Ground curGround = _groundDic[hashCode];

            if (_previousGround == null ||
                curGround != _previousGround)
            {
                UpdateGroundColor(curGround);
            }

            if (_curBuilding.BuildingItemInfoCompo.BuildingTypeEnum == // ���� �ǹ��̸� �׳� ���콺 ���󰡰� 
                BuildingType.Trap)
            {
                MoveTrap(hit.point);
                _curBuilding.ChangeToTransparencyMat(OutlineColorType.Green); // ���� �ǹ��̸� ������ ���� �ǹ��� ��ġ�Ǿ� �־ ��ġ �Ǿ� �ϴϱ�
            }
            else
            {
                MoveSelectBuilding(curGround);
            }

            _previousGround = curGround;
        }
    }

    private void MoveSelectBuilding(Ground curGround) // �׸���� �����̰�
    {
        Vector3 buildingPos = new Vector3(curGround.transform.position.x, 0f, curGround.transform.position.z);
        Vector3Int gridPosition = _curBuilding.BuildingInfoCompo.GridCompo.WorldToCell(buildingPos);
        //_curBuilding.transform.position = _curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition); // �׸���� �̵�
        _curBuilding.transform.position = new Vector3(_curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition).x,
                                                      1.5f,
                                                      _curBuilding.BuildingInfoCompo.GridCompo.CellToWorld(gridPosition).z);
    }

    private void MoveTrap(Vector3 pos)
    {
        _curBuilding.transform.position = pos;
    }

    private void UpdateGroundColor(Ground curGround) // ���� Outline ���� �ٲ��ִ� �Լ�
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
