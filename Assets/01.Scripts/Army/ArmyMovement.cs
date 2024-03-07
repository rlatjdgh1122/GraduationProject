using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArmyMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private ParticleSystem ClickParticle;
    private Army curArmy = null;

    public List<Entity> armySoldierList = new List<Entity>();

    private bool isCanMove = false;
    private bool successfulSeatMyPos = false;
    private bool BattleMode => ArmyManager.Instance.BattleMode;

    //움직이지 않고 모두가 위치에 이동했다면
    private bool result => successfulSeatMyPos && isCanMove;

    private Coroutine WaitForAllTrueCoutine = null;
    private Coroutine AllTrueToCanMoveCoutine = null;
    private Coroutine AllTrueToSeatMyPostionCoutine = null;

    private static float heartbeat = 0.1f;
    private static WaitForSecondsRealtime waitingByheartbeat = new WaitForSecondsRealtime(heartbeat);

    private void Awake()
    {
        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        _inputReader.RightClickEvent += SetClickMovement;
        SignalHub.OnArmyChanged += OnArmyChangedHandler;
    }

    private void OnArmyChangedHandler(Army prevArmy, Army newArmy)
    {
        curArmy = newArmy;
        SetArmyNumber();
    }

    private void SetArmyNumber()
    {
        if (armySoldierList.Count > 0)
            armySoldierList.Clear();

        for (int i = 0; i < curArmy.Soldiers.Count; ++i)
        {
            armySoldierList.Add(curArmy.Soldiers[i]);
        }

        if (curArmy.General != null)
        {
            armySoldierList.Add(curArmy.General);
        }
    }

    public void SetClickMovement()
    {
        RaycastHit hit;

        if (Physics.Raycast(GameManager.Instance.RayPosition(), out hit))
        {
            if (WaitForAllTrueCoutine != null)
                StopCoroutine(WaitForAllTrueCoutine);

            WaitForAllTrueCoutine = StartCoroutine(WaitForAllTrue_Corou(hit.point));

            ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
            ClickParticle.Play();
        }
    }

    private IEnumerator WaitForAllTrue_Corou(Vector3 mousePos)
    {
        isCanMove = false;
        successfulSeatMyPos = false;

        curArmy.IsCanReadyAttackInCurArmySoldiersList = false;

        foreach (var item in armySoldierList)
        {
            item.ArmyTriggerCalled = true;
            item.BattleMode = BattleMode;
        }

        //모두가 움직일 수 있는 상태인지 확인하기 위해 코루틴 돌려줌
        if (AllTrueToCanMoveCoutine != null)
            StopCoroutine(AllTrueToCanMoveCoutine);

        AllTrueToCanMoveCoutine = StartCoroutine(AllTrueToCanMove_Corou(mousePos));

        yield return new WaitUntil(() => result == true);

        // 성공적으로 해결되었다면
        curArmy.IsCanReadyAttackInCurArmySoldiersList = true;
    }
    private IEnumerator AllTrueToCanMove_Corou(Vector3 mousePos)
    {
        var check = false;
        isCanMove = false;

        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            Debug.Log("문제가 있다1");
        }
        while (!check)
        {
            foreach (var item in armySoldierList)
            {
                //공격 애니메이션이 끝났다면 움직일 수 있음
                if (item.WaitTrueAnimEndTrigger)
                {
                    check = true;
                    //움직여주기
                    SetSoldierMovePosition(mousePos, item);
                }
                else
                {
                    check = false;
                }
            }

            //모두가 위치로 움직일 수 있을때까지 대기
            yield return waitingByheartbeat;
        }

        //모두가 움직일 수 있다면
        // 모두가 자리에 위치해 있는지 확인하기 위해 코루틴을 돌려줌 

        isCanMove = true;

        if (AllTrueToSeatMyPostionCoutine != null)
            StopCoroutine(AllTrueToSeatMyPostionCoutine);

        AllTrueToSeatMyPostionCoutine = StartCoroutine(AllTrueToSeatMyPostion_Corou());
    }

    private IEnumerator AllTrueToSeatMyPostion_Corou()
    {
        successfulSeatMyPos = false;

        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            Debug.Log("문제가 있다2");
        }
        while (!armySoldierList.TrueForAll(p => p.SuccessfulToSeatMyPostion))
        {
            yield return waitingByheartbeat;
        }
        successfulSeatMyPos = true;
    }

    private void SetSoldierMovePosition(Vector3 mousePos, Entity entity)
    {
        entity.MoveToMySeat(mousePos);
    }

    /// <summary>
    /// 배치대로 이동
    /// </summary>
    /// <param name="mousePos"> 마우스 위치</param>
    private void SetArmyMovePostiton(Vector3 mousePos)
    {
        var soldiers = curArmy.Soldiers;
        var general = curArmy.General;

        if (general != null)
            general.MoveToMySeat(mousePos);

        foreach (var soldier in soldiers)
        {
            soldier.MoveToMySeat(mousePos);
        }
    }


    private void OnDestroy()
    {
        _inputReader.RightClickEvent -= SetClickMovement;
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }
}
