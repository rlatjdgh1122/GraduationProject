using Define.RayCast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PenguinMovementInfo
{
    public PenguinMovementInfo(bool isCheck, Penguin obj)
    {
        IsCheck = isCheck;
        Obj = obj;
    }

    public bool IsCheck;
    public Penguin Obj;
}
public class ArmyMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private ParticleSystem ClickParticle;
    private Army curArmy = null;

    public List<PenguinMovementInfo> armySoldierList = new();

    private bool isCanMove = false;
    private bool successfulSeatMyPos = false;

    //�������� �ʰ� ��ΰ� ��ġ�� �̵��ߴٸ�
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
        SignalHub.OnModifyArmyInfo += OnModifyArmyInfoHnadler;
    }

    private void OnArmyChangedHandler(Army prevArmy, Army newArmy)
    {
        curArmy = newArmy;
        SetArmyNumber();
    }
    private void OnModifyArmyInfoHnadler()
    {
        curArmy = ArmyManager.Instance.GetCurArmy();
        SetArmyNumber();
    }

    private void SetArmyNumber()
    {
        if (armySoldierList.Count < 0) return;

        if (armySoldierList.Count > 0)
            armySoldierList.Clear();

        //장군이 있다면 장군도 추가
        if (curArmy.General)
        {
            PenguinMovementInfo armySoldier =
                new(false, curArmy.General);

            armySoldierList.Add(armySoldier);
        }

        //군사들 추가
        for (int i = 0; i < curArmy.Soldiers.Count; ++i)
        {
            PenguinMovementInfo armySoldier =
                new(false, curArmy.Soldiers[i]);
            armySoldierList.Add(armySoldier);
        }
    }
    public void SetClickMovement()
    {
        RaycastHit hit;
        if (Physics.Raycast(RayCasts.MousePointRay, out hit))
        {
            if (WaitForAllTrueCoutine != null)
                StopCoroutine(WaitForAllTrueCoutine);

            WaitForAllTrueCoutine = StartCoroutine(WaitForAllTrue_Corou(hit.point));

            ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
            ClickParticle.Play();
        }
    }

    /// <summary>
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

        curArmy.IsCanReadyAttackInCurArmySoldiersList = false;

        foreach (var item in armySoldierList)
        {
            item.IsCheck = false;
            var obj = item.Obj;

            obj.ArmyTriggerCalled = true;
            obj.MousePos = mousePos;
        }

        if (AllTrueToCanMoveCoutine != null)
            StopCoroutine(AllTrueToCanMoveCoutine);

        AllTrueToCanMoveCoutine = StartCoroutine(AllTrueToCanMove_Corou(mousePos));

        yield return new WaitUntil(() => result == true);

        curArmy.IsCanReadyAttackInCurArmySoldiersList = true;
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
            Debug.Log("������ �ִ�1");
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

            //��ΰ� ��ġ�� ������ �� ���������� ���
            yield return waitingByheartbeat;
        }

        //��ΰ� ������ �� �ִٸ�
        // ��ΰ� �ڸ��� ��ġ�� �ִ��� Ȯ���ϱ� ���� �ڷ�ƾ�� ������ 

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
            Debug.Log("������ �ִ�2");
        }

        //성공적으로 위치에 도달할때까지 무한 반복
        while (!armySoldierList.TrueForAll(p
            => p.Obj.SuccessfulToArmyCalled))
        {
            yield return waitingByheartbeat;
        }
        successfulSeatMyPos = true;
    }

    private void SetSoldierMovePosition(Vector3 mousePos, Penguin penguin)
    {
        penguin.MoveToMySeat(mousePos);
    }

    private void OnDestroy()
    {
        _inputReader.RightClickEvent -= SetClickMovement;
        //SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }

    private void OnDisable()
    {
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }
}
