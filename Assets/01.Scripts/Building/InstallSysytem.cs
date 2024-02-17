using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstallSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private BuildingDatabaseSO _buildingDatabaseSO;
    private int selectedBuildingIDX = -1;

    [SerializeField]
    private InputReader _inputReader;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //ĳ�̿� ��ųʸ�

    private bool isInstalling;

    private void Start()
    {
        StopInstall();
    }

    public void SelectBuilding(BaseBuilding building)
    {
        building.SetSelected();

        StartInstall(building.BuildingInfoCompo.ID);
    }

    private void StartInstall(int id)
    {
        selectedBuildingIDX = _buildingDatabaseSO.BuildingItems.FindIndex(building => building.ID == id);

        if (selectedBuildingIDX <= 0)
        {
            Debug.LogError($"No id found {id}");
            return;
        }

        _inputReader.OnLeftClickEvent += PlaceStructure;
        _inputReader.OnExitInstallEvent += StopInstall;
    }

    private void StopInstall()
    {
        selectedBuildingIDX = -1;
        _inputReader.OnLeftClickEvent -= PlaceStructure;
        _inputReader.OnExitInstallEvent -= StopInstall;
    }

    private void PlaceStructure()
    {
        if (_inputReader.IsPointerOverUI())
        {
            return;
        }

        if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _groundLayer))
        {
            BaseBuilding curSelectedBuilding = PoolManager.Instance.Pop(_buildingDatabaseSO.BuildingItems[selectedBuildingIDX].Name) as BaseBuilding;

            Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
            Vector3Int gridPosition = curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
            curSelectedBuilding.transform.position = curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // �׸���� �̵�
        }
    }

    private void Update()
    {
        if (selectedBuildingIDX < 0) { return; }


    }

    private IEnumerator BuildingFollowMousePosition(BaseBuilding curSelectedBuilding)
    {
        while (true)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _groundLayer))
            {
                if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // ĳ��
                {
                    _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
                }

                Ground _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                // ���� �̸� ��ġ�Ǿ� ������ 
                if (_curGround.IsInstalledBuilding && !_curGround.IsRedMT)
                {
                    Debug.Log("This ground already installed");
                    _curGround.ShowInstallPossibility(false); // ��ġ �Ұ����ϴٰ� ��Ÿ��
                    yield return null;
                }
                else
                {
                    if (!_curGround.IsGreenMT) { _curGround.ShowInstallPossibility(true); } // ��ġ �����ϴٰ� ��Ÿ��

                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    curSelectedBuilding.transform.position = curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // �׸���� �̵�

                    if (Input.GetMouseButtonDown(0)) // �ѹ� �� ������ ��ġ
                    {
                        curSelectedBuilding.transform.SetParent(hit.transform);
                        curSelectedBuilding.Installed(); // �ǹ��� ��ġ ó��
                        _curGround.InstallBuilding(); // ���� ��ġ ó��

                        yield break; // �ڷ�ƾ ����
                    }
                }
            }

            yield return null;
        }
    }
}
