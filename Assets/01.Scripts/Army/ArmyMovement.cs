using Define.RayCast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArmySystem;

public class ArmyMovement : MonoBehaviour
{
    private Army curArmy = null;
    public List<Penguin> armySoldierList = new();

    private bool isCanMove = false;
    private bool successfulSeatMyPos = false;

    private bool result => successfulSeatMyPos && isCanMove;

    private Coroutine WaitForAllTrueCoutine = null;
    private Coroutine AllTrueToCanMoveCoutine = null;
    private Coroutine AllTrueToSeatMyPostionCoutine = null;

    private static float heartbeat = 0.1f;
    private static WaitForSecondsRealtime waitingByheartbeat = new WaitForSecondsRealtime(heartbeat);

    private void OnEnable()
    {
        SignalHub.OnArmyChanged += OnArmyChangedHandler;
        SignalHub.OnModifyCurArmy += OnModifyCurArmyHnadler;

    }
    private void OnDisable()
    {
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
        SignalHub.OnModifyCurArmy -= OnModifyCurArmyHnadler;
    }
   

    private void OnArmyChangedHandler(Army prevArmy, Army newArmy)
    {
        curArmy = newArmy;
        SetArmyNumber();
    }

    private void OnModifyCurArmyHnadler()
    {
        curArmy = ArmyManager.Instance.CurArmy;
        SetArmyNumber();
    }

    private void SetArmyNumber()
    {
        if (armySoldierList.Count < 0) return;

        armySoldierList.TryClear();

        //장군이 있다면 장군도 추가
        if (curArmy.General)
        {
            armySoldierList.Add(curArmy.General);
        }

        //군사들 추가
        for (int i = 0; i < curArmy.Soldiers.Count; ++i)
        {
            armySoldierList.Add(curArmy.Soldiers[i]);
        }
    }

    public void OnClickForMovement(RaycastHit hit)
    {
        if (WaitForAllTrueCoutine != null) StopCoroutine(WaitForAllTrueCoutine);
        WaitForAllTrueCoutine = StartCoroutine(Movement_Corou(hit.point));
    }

    private IEnumerator Movement_Corou(Vector3 mousePos)
    {
        if (armySoldierList.Count <= 0)
            yield break;

        curArmy.IsArmyReady = false;

        foreach (var penguin in armySoldierList)
        {
            penguin.MousePos = mousePos;
            penguin.SuccessfulToArmyCalled = false;
            penguin.ArmyTriggerCalled = true;
            penguin.MoveToMySeat(mousePos);
        }

        yield return new WaitUntil(() => armySoldierList.TrueForAll(penguin => penguin.SuccessfulToArmyCalled));

        curArmy.IsArmyReady = true;
    }

    #region 이전 코드 
    /*/// <summary>
    /// 군단 모두가 자리에 도착할때까지 대기
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    private IEnumerator WaitForAllTrue_Corou(Vector3 mousePos)
    {
        if (armySoldierList.Count <= 0)
            yield break;

        isCanMove = false;
        successfulSeatMyPos = false;
        curArmy.IsArmyReady = false;

        foreach (var item in armySoldierList)
        {
            item.IsCheck = false;
            var obj = item.Obj;
            obj.MousePos = mousePos;
        }

        if (AllTrueToCanMoveCoutine != null)
            StopCoroutine(AllTrueToCanMoveCoutine);

        AllTrueToCanMoveCoutine = StartCoroutine(AllTrueToCanMove_Corou(mousePos));

        yield return new WaitUntil(() => result == true);

        curArmy.IsArmyReady = true;
    }

    /// <summary>
    /// 군단 모두가 움직 일 수 있을 때 까지 대기
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    private IEnumerator AllTrueToCanMove_Corou(Vector3 mousePos)
    {
        var check = false;
        isCanMove = false;

        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            Debug.Log("현재 선택된 군단에 네브메쉬가 없는 펭귄이 존재합니다.");
        }
        while (!check)
        {
            foreach (var item in armySoldierList)
            {

                if (item.Obj.WaitForCommandToArmyCalled)
                {
                    check = true;

                    if (!item.IsCheck)
                        SetSoldierMovePosition(mousePos, item.Obj);

                    item.IsCheck = true;
                }
                else
                {
                    check = false;
                }
            }

            yield return waitingByheartbeat;
        }

        isCanMove = true;

        if (AllTrueToSeatMyPostionCoutine != null)
            StopCoroutine(AllTrueToSeatMyPostionCoutine);

        AllTrueToSeatMyPostionCoutine = StartCoroutine(AllTrueToSeatMyPostion_Corou());
    }

    /// <summary>
    /// 군단이 모두 움직일 때 까지 대기
    /// </summary>
    /// <returns></returns>
    private IEnumerator AllTrueToSeatMyPostion_Corou()
    {
        successfulSeatMyPos = false;

        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            Debug.Log("현재 선택된 군단에 네브메쉬가 없는 펭귄이 존재합니다.");
        }

        //성공적으로 위치에 도달할때까지 무한 반복
        while (!armySoldierList.TrueForAll(p
            => p.Obj.SuccessfulToArmyCalled))
        {
            yield return waitingByheartbeat;
        }
        successfulSeatMyPos = true;
    }*/
    #endregion

    private void OnDestroy()
    {
        if (armySoldierList.Count > 0)
            armySoldierList.Clear();
    }
}
