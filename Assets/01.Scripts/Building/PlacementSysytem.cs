using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSysytem : MonoBehaviour
{
    private Coroutine _followMousePositionCoroutine;

    private BaseBuilding _curBuilding;

    public void SelectBuilding(BaseBuilding building)
    {
        building.SetSelected();
        _curBuilding = building;

        _followMousePositionCoroutine = StartCoroutine(FollowMousePosition());
    }

    private IEnumerator FollowMousePosition()
    {
        while (true)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity))
            {
                Vector3Int gridPosition = _curBuilding.BuildingInfoCompo.Grid.WorldToCell(hit.point);
                _curBuilding.transform.position = _curBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition);
            }

            yield return 0.1f;
        }
    }

    
}
