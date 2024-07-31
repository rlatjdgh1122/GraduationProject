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

    public HealingController(Transform transform, Vector3 startPos, Vector3 endPos, float DetectionRange)
    {
        _transform = transform;
        _spawnStartPostion = startPos;
        _spawnEndPostion = endPos;
        _detectionRange = DetectionRange;

        _targetLayer = LayerMask.NameToLayer("Player");
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
            (_checkCount == _seletedArmy.AlivePenguins.Count, //�� �ɶ�����
            () =>                                             // �ݺ��ؼ� ����� ���ٽ�
            {
                _checkCount = Physics.OverlapSphereNonAlloc(_transform.position, _detectionRange, _colls, _targetLayer);

                for (int i = 0; i < _checkCount; i++)
                {
                    Collider col = _colls[i];

                    // ������ ������Ʈ ��Ȱ��ȭ
                    col.gameObject.SetActive(false);
                }
            },
            0.02f,                                            //�� �ֱ��
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
