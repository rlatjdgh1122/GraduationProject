using ArmySystem;
using System;
using UnityEngine;

public class HealingController
{
    private Army _seletedArmy = null;
    private Transform _spawnStartPostion = null;
    private Transform _spawnEndPostion = null;

    public HealingController(Transform startPos, Transform endPos)
    {
        _spawnStartPostion = startPos;
        _spawnEndPostion = endPos;
    }

    public void SetArmy(Army army)
    {
        _seletedArmy = army;
    }


    public void GoToBuilding(Action afterAction)
    {
        // ���⼭ ������ �Ѵ��� 
        // ������ ���� �ֵ��� ������Ʈ�� ���ְ�
        // ����ִ� �Ʊ��� �� ��������
        //CoroutineUtil.DoCallWaitForAction(, afterAction);
    }

    public void LeaveBuilding()
    {

    }

}
