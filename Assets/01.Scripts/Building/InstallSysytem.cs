using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstallSysytem : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private Dictionary<int, Ground> _groundDic = new Dictionary<int, Ground>(); //ĳ�̿� ��ųʸ�

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
                if (!_groundDic.ContainsKey(hit.transform.gameObject.GetHashCode())) // ĳ��
                {
                    _groundDic.Add(hit.transform.gameObject.GetHashCode(), hit.transform.GetComponent<Ground>());
                }

                Ground _curGround = _groundDic[hit.transform.gameObject.GetHashCode()];

                // ���� �̸� ��ġ�Ǿ� ������ 
                if (_curGround.IsInstalledBuilding)
                {
                    Debug.Log("This ground already installed");
                    _curGround.ShowInstallPossibility(false); // ��ġ �Ұ����ϴٰ� ��Ÿ��
                    yield return null;
                }
                else
                {
                    _curGround.ShowInstallPossibility(true); // ��ġ �����ϴٰ� ��Ÿ��
                    Vector3 hitPos = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    Vector3Int gridPosition = curSelectedBuilding.BuildingInfoCompo.Grid.WorldToCell(hitPos);
                    curSelectedBuilding.transform.position = curSelectedBuilding.BuildingInfoCompo.Grid.CellToWorld(gridPosition); // �׸���� �̵�

                    if (Input.GetMouseButtonDown(0) && curSelectedBuilding.gameObject.activeInHierarchy) // �ѹ� �� ������ ��ġ
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
