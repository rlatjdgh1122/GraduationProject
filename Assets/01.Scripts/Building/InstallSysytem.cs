using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstallSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private BaseBuilding _curSelectedBuilding;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //캐싱용 딕셔너리

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
                if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // 캐싱
                {
                    _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
                }

                Ground curGround = null;
                curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                // 만약 미리 설치되어 있으면 
                if (curGround.IsInstalledBuilding)
                {
                    Debug.Log("이미 설치되어 있음");
                    curGround.ShowInstallPossibility(false); // 설치 불가능하다고 나타냄
                    yield return null;
                }
                else if (!curGround.IsInstalledBuilding && _curSelectedBuilding != null)
                {
                    curGround.ShowInstallPossibility(true); // 설치 가능하다고 나타냄
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = _curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    _curSelectedBuilding.transform.position = _curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // 그리드로 이동

                    if (Input.GetMouseButtonDown(0)) // 한번 더 누르면 설치
                    {
                        _curSelectedBuilding.transform.SetParent(hit.transform);
                        _curSelectedBuilding.Installed(); // 건물에 설치 처리
                        curGround.InstallBuilding(); // 땅에 설치 처리

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
