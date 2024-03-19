using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.RayCast;
public class ArmyMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private ParticleSystem ClickParticle;
    private Army curArmy = null;

    public List<Penguin> armySoldierList = new List<Penguin>();

    private bool isCanMove = false;
    private bool successfulSeatMyPos = false;
    private MovefocusMode CurFocusMode => ArmyManager.Instance.CurFocusMode;

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
            armySoldierList.Add(curArmy.Soldiers[i]);
        }

        if (curArmy.General != null)
        {
            armySoldierList.Add(curArmy.General);
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
            item.ArmyTriggerCalled = true;
            //item.MoveFocusMode = CurFocusMode;
            item.MousePos = mousePos;

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
                //���� �ִϸ��̼��� �����ٸ� ������ �� ����
                if (item.WaitForCommandToArmyCalled)
                {
                    check = true;
                    SetSoldierMovePosition(mousePos, item);
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
        while (!armySoldierList.TrueForAll(p => p.SuccessfulToArmyCalled))
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
