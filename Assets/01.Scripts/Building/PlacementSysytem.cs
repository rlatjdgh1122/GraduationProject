using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private BaseBuilding _curSelectedBuilding;
    private Ground _curGround;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //ĳ�̿� ��ųʸ�

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
                if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // ĳ��
                {
                    _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
                    Debug.Log("Cash");
                }

                _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                //���� �̸� ��ġ�Ǿ� ������ 
                if (_curGround.IsPlacedBuilding)
                {
                    Debug.Log("�̹� ��ġ�Ǿ� ����");
                    yield return null;
                }
                else
                {
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = _curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    _curSelectedBuilding.transform.position = _curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // �׸���� �̵�

                    if (Input.GetMouseButtonDown(0) && _curSelectedBuilding.gameObject.activeInHierarchy) // �ѹ� �� ������ ��ġ
                    {
                        _curSelectedBuilding.transform.SetParent(hit.transform);
                        _curSelectedBuilding.transform.position = hitPos;
                        _curSelectedBuilding.Placed(); // �ǹ��� ��ġ ó��
                        _curGround.PlacedBuilding(); // ���� ��ġ ó��

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
