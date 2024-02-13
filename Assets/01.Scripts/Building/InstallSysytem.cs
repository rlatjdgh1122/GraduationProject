using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstallSysytem : MonoBehaviour
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
                }

                _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                //���� �̸� ��ġ�Ǿ� ������ 
                if (_curGround.IsInstalledBuilding)
                {
                    Debug.Log("�̹� ��ġ�Ǿ� ����");
                    _curGround.ShowInstallPossibility(false); // ��ġ �Ұ����ϴٰ� ��Ÿ��
                    yield return new WaitForSeconds(0.25f);
                }
                else
                {
                    _curGround.ShowInstallPossibility(true); // ��ġ �����ϴٰ� ��Ÿ��
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = _curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    _curSelectedBuilding.transform.position = _curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // �׸���� �̵�

                    if (Input.GetMouseButtonDown(0) && _curSelectedBuilding.gameObject.activeInHierarchy) // �ѹ� �� ������ ��ġ
                    {
                        _curSelectedBuilding.transform.SetParent(hit.transform);
                        _curSelectedBuilding.transform.position = hitPos;
                        _curSelectedBuilding.Installed(); // �ǹ��� ��ġ ó��
                        _curGround.InstallBuilding(); // ���� ��ġ ó��

                        _curGround = null;
                        _curSelectedBuilding = null;
                        yield break;
                    }

                }
            }
            yield return null;
        }
    }

    private IEnumerator TempCorou()
    {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Test");
    }
}
