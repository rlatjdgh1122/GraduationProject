using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstallSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private BaseBuilding _curSelectedBuilding;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //ĳ�̿� ��ųʸ�

    private bool _buildingPlacementInProgress = false;

    public void SelectBuilding(BaseBuilding building)
    {
        if (!_buildingPlacementInProgress)
        {
            _curSelectedBuilding = building;
            building.SetSelected();

            StartCoroutine(BuildingFollowMousePosition());
        }
    }

    private IEnumerator BuildingFollowMousePosition()
    {
        _buildingPlacementInProgress = true;

        while (_curSelectedBuilding != null)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _groundLayer))
            {
                if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // ĳ��
                {
                    _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
                }

                Ground curGround = null;
                curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                // ���� �̸� ��ġ�Ǿ� ������ 
                if (curGround.IsInstalledBuilding)
                {
                    Debug.Log("�̹� ��ġ�Ǿ� ����");
                    curGround.ShowInstallPossibility(false); // ��ġ �Ұ����ϴٰ� ��Ÿ��
                    yield return null;
                }
                else if (!curGround.IsInstalledBuilding && _curSelectedBuilding != null)
                {
                    curGround.ShowInstallPossibility(true); // ��ġ �����ϴٰ� ��Ÿ��
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = _curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    _curSelectedBuilding.transform.position = _curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // �׸���� �̵�

                    if (Input.GetMouseButtonDown(0)) // �ѹ� �� ������ ��ġ
                    {
                        _curSelectedBuilding.transform.SetParent(hit.transform);
                        _curSelectedBuilding.Installed(); // �ǹ��� ��ġ ó��
                        curGround.InstallBuilding(); // ���� ��ġ ó��

                        _curSelectedBuilding = null;

                        _buildingPlacementInProgress = false;
                        yield break;
                    }
                }
            }

            yield return null;
        }

        _buildingPlacementInProgress = false;
    }
}
