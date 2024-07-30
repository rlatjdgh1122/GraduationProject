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
        // 여기서 감지를 한다음 
        // 감지에 들어온 애들은 오브젝트를 꺼주고
        // 살아있는 아군이 다 들어왔으면
        //CoroutineUtil.DoCallWaitForAction(, afterAction);
    }

    public void LeaveBuilding()
    {

    }

}
