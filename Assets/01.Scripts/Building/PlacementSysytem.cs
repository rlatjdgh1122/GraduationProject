using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private Coroutine _followMousePositionCoroutine;

    private BaseBuilding _curBuilding;

    public void SelectBuilding(BaseBuilding building)
    {
        building.SetSelected();
        _curBuilding = building;

        _followMousePositionCoroutine = StartCoroutine(BuildingFollowMousePosition());
    }

    private IEnumerator BuildingFollowMousePosition()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _curBuilding.Placed();
                break;
            }

            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _groundLayer))
            {
                Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                Vector3Int gridPosition = _curBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                _curBuilding.transform.position = _curBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    
}
