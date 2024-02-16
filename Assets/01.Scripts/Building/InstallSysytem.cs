using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstallSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //캐싱용 딕셔너리

    public void SelectBuilding(BaseBuilding building)
    {
        building.SetSelected();

        StartCoroutine(BuildingFollowMousePosition(building));
    }

    private IEnumerator BuildingFollowMousePosition(BaseBuilding curSelectedBuilding)
    {
        while (true)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _groundLayer))
            {
                if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // 캐싱
                {
                    _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
                }

                Ground _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                // 만약 미리 설치되어 있으면 
                if (_curGround.IsInstalledBuilding)
                {
                    Debug.Log("This ground already installed");
                    _curGround.ShowInstallPossibility(false); // 설치 불가능하다고 나타냄
                    yield return null;
                }
                else
                {
                    _curGround.ShowInstallPossibility(true); // 설치 가능하다고 나타냄
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    curSelectedBuilding.transform.position = curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // 그리드로 이동

                    if (Input.GetMouseButtonDown(0) && curSelectedBuilding.gameObject.activeInHierarchy) // 한번 더 누르면 설치
                    {
                        curSelectedBuilding.transform.SetParent(hit.transform);
                        curSelectedBuilding.Installed(); // 건물에 설치 처리
                        _curGround.InstallBuilding(); // 땅에 설치 처리

                        yield break; // 코루틴 종료
                    }
                }
            }

            yield return null;
        }
    }
}
