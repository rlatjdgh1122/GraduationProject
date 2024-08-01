using ArmySystem;
using System;
using UnityEngine;

public class HealingController
{
    private Army _seletedArmy = null;
    private Vector3 _spawnStartPostion = Vector3.zero;
    private Vector3 _spawnEndPostion = Vector3.zero;
    private Transform _transform = null;

    private Collider[] _colls = new Collider[10]; //���� 10
    private float _detectionRange = 0f;
    private LayerMask _targetLayer;
    private int _checkCount = 0;

    public HealingController(Transform rootTrm, Vector3 startPos, Vector3 endPos, float DetectionRange)
    {
        _transform = rootTrm;
        _spawnStartPostion = startPos;
        _spawnEndPostion = endPos;
        _detectionRange = DetectionRange;

        _targetLayer = 1 << LayerMask.NameToLayer("Player");
    }

    public void SetArmy(Army army)
    {
        _seletedArmy = army;
    }


    public void GoToBuilding(Action afterAction)
    {

        foreach (var penguin in _seletedArmy.AlivePenguins)
        {
            penguin.MustMoveToTargetPostion(_transform.position);
        }
        // ���⼭ ������ �Ѵ���
        // ������ ���� �ֵ��� ������Ʈ�� ���ְ�
        // �� �������� afterAction�� ��������

        CoroutineUtil.CallWaitForActionUntilTrue
            (
            () => _checkCount == _seletedArmy.AlivePenguins.Count, //�� �ɶ�����
            () =>                                             // �ݺ��ؼ� ����� ���ٽ�
            {
                int count = Physics.OverlapSphereNonAlloc(_transform.position, _detectionRange, _colls, _targetLayer);

                for (int i = 0; i < count; i++)
                {
                    Collider col = _colls[i];

                    // ������ ������Ʈ ��Ȱ��ȭ
                    if (col.gameObject.activeSelf)
                    {
                        col.gameObject.SetActive(false);
                        _checkCount++;
                    }
                }
            },
            0.2f,                                            //�� �ֱ��
            afterAction);                                     //������ �����ϸ� ����
    }


    /// <summary>
    /// ������ ������ ���� �ֵ� �ٻ츲
    /// </summary>
    public void EndHealing()
    {
        var DeadLists = _seletedArmy.DeadPenguins.ToArray();

        foreach (var penguin in DeadLists)
        {
            penguin.OnResurrected();
            _seletedArmy.ResurrectPenguin(penguin);
        }

        LeaveBuilding();
    }

    public void BrokenBuilding()
    {
        LeaveBuilding();
    }

    private void LeaveBuilding()
    {
        _checkCount = 0;

        var AliveLists = _seletedArmy.AlivePenguins.ToArray();

        foreach (var penguin in AliveLists)
        {
            penguin.transform.position = _spawnStartPostion;
            penguin.gameObject.SetActive(true);
            penguin.IsMustMoving = false;

            penguin.MoveToMySeat(_spawnEndPostion);
        }
    }



}
