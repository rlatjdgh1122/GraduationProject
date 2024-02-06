using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private BaseBuilding _curBuilding;
    private Ground _curGround;

    public void SelectBuilding(BaseBuilding building)
    {
        building.SetSelected();
        _curBuilding = building;

        StartCoroutine(BuildingFollowMousePosition());
    }

    private IEnumerator BuildingFollowMousePosition()
    {
        while (true)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _groundLayer))
            {
                _curGround = hit.transform.GetComponent<Ground>();
                //만약 미리 설치되어 있으면 
                if (_curGround.IsPlacedBuilding)
                {
                    Debug.Log("이미 설치되어 있음");
                    yield return null;
                }
                else
                {
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = _curBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    _curBuilding.transform.position = _curBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition);

                    if (Input.GetMouseButtonDown(0) && _curBuilding.gameObject.activeInHierarchy)
                    {
                        _curBuilding.Placed();
                        _curBuilding.transform.SetParent(hit.transform);
                        _curGround.PlacedBuilding();

                        _curGround = null;
                        _curBuilding = null;
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }
}
