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

        //��ΰ� ������ �� �ִ� �������� Ȯ���ϱ� ���� �ڷ�ƾ ������
        if (AllTrueToCanMoveCoutine != null)
            StopCoroutine(AllTrueToCanMoveCoutine);

        AllTrueToCanMoveCoutine = StartCoroutine(AllTrueToCanMove_Corou(mousePos));

        yield return new WaitUntil(() => result == true);

        // ���������� �ذ�Ǿ��ٸ�
        Debug.Log("������");
        //������� ������ �ȵǴµ���
        curArmy.IsCanReadyAttackInCurArmySoldiersList = true;
    }
    private IEnumerator AllTrueToCanMove_Corou(Vector3 mousePos)
    {
        isCanMove = false;

        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            Debug.Log("������ �ִ�1");
        }

        while (!isCanMove)
        {
            foreach (var item in armySoldierList)
            {
                //���� �ִϸ��̼��� �����ٸ� ������ �� ����
                if (item.Soldier.WaitTrueAnimEndTrigger)
                {
                    isCanMove = true;
                    //�������ֱ�
                    SetSoldierMovePosition(mousePos, item.Soldier);
                }
                else
                {
                    isCanMove = false;
                }
            }

            //��ΰ� ��ġ�� ������ �� ���������� ���
            yield return waitingByheartbeat;
        }

        //��ΰ� ������ �� �ִٸ�
        // ��ΰ� �ڸ��� ��ġ�� �ִ��� Ȯ���ϱ� ���� �ڷ�ƾ�� ������ 
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

        while (!successfulSeatMyPos)
        {
            foreach (var item in armySoldierList)
            {
                //���� �ִϸ��̼��� �����ٸ� ������ �� ����
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
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }
}
