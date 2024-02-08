using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private BaseBuilding _curSelectedBuilding;
    private Ground _curGround;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //캐싱용 딕셔너리

    public void SelectBuilding(BaseBuilding building)
    {
        building.SetSelected();
        _curSelectedBuilding = building;

        StartCoroutine(BuildingFollowMousePosition());
    }

    private IEnumerator BuildingFollowMousePosition()
    {
        while (true)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _groundLayer))
            {
                if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // 캐싱
                {
                    _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
                    Debug.Log("Cash");
                }

                _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                //만약 미리 설치되어 있으면 
                if (_curGround.IsPlacedBuilding)
                {
                    Debug.Log("이미 설치되어 있음");
                    yield return null;
                }
                else
                {
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = _curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    _curSelectedBuilding.transform.position = _curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // 그리드로 이동

                    if (Input.GetMouseButtonDown(0) && _curSelectedBuilding.gameObject.activeInHierarchy) // 한번 더 누르면 설치
                    {
                        _curSelectedBuilding.transform.SetParent(hit.transform);
                        _curSelectedBuilding.transform.position = hitPos;
                        _curSelectedBuilding.Placed(); // 건물에 설치 처리
                        _curGround.PlacedBuilding(); // 땅에 설치 처리

                        _curGround = null;
                        _curSelectedBuilding = null;
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }
}
