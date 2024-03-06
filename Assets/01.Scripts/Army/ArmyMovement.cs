using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmyNumber
{
    public Entity Soldier;
    public bool check;
}

public class ArmyMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private ParticleSystem ClickParticle;
    private Army curArmy = null;

    public List<ArmyNumber> armySoldierList = new List<ArmyNumber>();

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
    private void Start()
    {
        curArmy = ArmyManager.Instance.GetCurArmy();
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
            var soldier = new ArmyNumber();
            soldier.Soldier = curArmy.Soldiers[i];
            soldier.check = false;

            armySoldierList.Add(soldier);
        }

        if (curArmy.General != null)
        {
            var soldier = new ArmyNumber();
            soldier.Soldier = curArmy.General;
            soldier.check = false;

            armySoldierList.Add(soldier);
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
            item.Soldier.ArmyTriggerCalled = true;
            item.Soldier.BattleMode = battleMode;
        }

        //모두가 움직일 수 있는 상태인지 확인하기 위해 코루틴 돌려줌
        if (AllTrueToCanMoveCoutine != null)
            StopCoroutine(AllTrueToCanMoveCoutine);

        AllTrueToCanMoveCoutine = StartCoroutine(AllTrueToCanMove_Corou(mousePos));

        yield return new WaitUntil(() => result == true);

        // 성공적으로 해결되었다면
        Debug.Log("오케이");
        //값복사라 적용이 안되는듯함
        curArmy.IsCanReadyAttackInCurArmySoldiersList = true;
    }
    private IEnumerator AllTrueToCanMove_Corou(Vector3 mousePos)
    {
        isCanMove = false;

        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            Debug.Log("문제가 있다1");
        }

        while (!isCanMove)
        {
            foreach (var item in armySoldierList)
            {
                //공격 애니메이션이 끝났다면 움직일 수 있음
                if (item.Soldier.WaitTrueAnimEndTrigger)
                {
                    isCanMove = true;
                    //움직여주기
                    SetSoldierMovePosition(mousePos, item.Soldier);
                }
                else
                {
                    isCanMove = false;
                }
            }

            //모두가 위치로 움직일 수 있을때까지 대기
            yield return waitingByheartbeat;
        }

        //모두가 움직일 수 있다면
        // 모두가 자리에 위치해 있는지 확인하기 위해 코루틴을 돌려줌 
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

        while (!successfulSeatMyPos)
        {
            foreach (var item in armySoldierList)
            {
                //공격 애니메이션이 끝났다면 움직일 수 있음
                if (item.Soldier.SuccessfulToSeatMyPostion)
                {
                    successfulSeatMyPos = true;
                }
                else
                {
                    successfulSeatMyPos = false;
                }
            }

            yield return waitingByheartbeat;
        }
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
