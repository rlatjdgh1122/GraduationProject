using ArmySystem;
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


    public void GoToBuilding()
    {
        //bool iss = true;
        //CoroutineUtil.DoCallWaitForAction(iss, null);
    }

    public void LeaveBuilding()
    {

    }

}
