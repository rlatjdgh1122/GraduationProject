using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.RayCast;
using UnityEngine.Rendering;

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
            PenguinMovementInfo armySoldier =
                new(false, curArmy.Soldiers[i]);
            armySoldierList.Add(armySoldier);
        }

        if (curArmy.General != null)
        {
            PenguinMovementInfo armySoldier =
                new(false, curArmy.General);
            armySoldierList.Add(armySoldier);
        }
    }
    public void SetArmy_Btn()
    {
        curArmy = ArmyManager.Instance.GetCurArmy();
        SetArmyNumber();
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
            //obj.SuccessfulToArmyCalled = false;
            obj.MousePos = mousePos;

        }

        //��ΰ� ������ �� �ִ� �������� Ȯ���ϱ� ���� �ڷ�ƾ ������
        if (AllTrueToCanMoveCoutine != null)
            StopCoroutine(AllTrueToCanMoveCoutine);

        AllTrueToCanMoveCoutine = StartCoroutine(AllTrueToCanMove_Corou(mousePos));

        yield return new WaitUntil(() => result == true);

        // ���������� �ذ�Ǿ��ٸ�
        curArmy.IsCanReadyAttackInCurArmySoldiersList = true;
    }
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

    private IEnumerator AllTrueToSeatMyPostion_Corou()
    {
        successfulSeatMyPos = false;

        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            Debug.Log("������ �ִ�2");
        }
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

    /// <summary>
    /// ��ġ��� �̵�
    /// </summary>
    /// <param name="mousePos"> ���콺 ��ġ</param>
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
        //SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }

    private void OnDisable()
    {
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }
}
