using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyNumber
{
    public Entity Soldier;
    public bool check;
}

public class ArmyMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float heartbeat = 0.1f;
    private ParticleSystem ClickParticle;
    private Army curArmy => ArmyManager.Instance.GetCurArmy();

    private List<ArmyNumber> armySoldierList = new List<ArmyNumber>();

    private bool _returnResult = false;
    private Coroutine WaitForAllTrueCoutine = null;

    private void Awake()
    {
        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        _inputReader.RightClickEvent += SetClickMovement;
        SignalHub.OnArmyChanged += OnArmyChangedHandler;
    }



    private void OnArmyChangedHandler(Army prevArmy, Army newArmy)
    {
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

            WaitForAllTrueCoutine = StartCoroutine(WaitForAllTrue(hit.point));

            //SetArmyMovePostiton(hit.point);
            ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
            ClickParticle.Play();
        }



    }

    private bool AllCanMovingInList() => _returnResult;
    private IEnumerator WaitForAllTrue(Vector3 mousePos)
    {
        // 리스트의 모든 AA 객체가 qwer가 true가 될 때까지 반복
        while (!AllTrueInList(mousePos))
        {
            // heartbeat 시간까지 게임시간 단위로 기다림

            var heartbeat = 2f;
            Time.timeScale = .5f;
            yield return new WaitForSecondsRealtime(heartbeat);
            // => 4초 기다림

        }

        // 모두 true일 때 여기로 진행
        Debug.Log("모두 true입니다!");
    }

    private bool AllTrueInList(Vector3 mousePos)
    {
        if (!curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
            return false;

        bool result = true;

        foreach (var item in armySoldierList)
        {
            if (item.Soldier.IsAbsoluteMovement)
            {
                result = false;
            }
            else
            {
                SetSoldierMovePosition(mousePos, item.Soldier);
            }
        }

        return result; // 모든 항목이 true일 경우 true 반환
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

    private void SetSoldierMovePosition(Vector3 mousePos, Entity entity)
    {
        entity.MoveToMySeat(mousePos);
    }

    private void OnDestroy()
    {
        _inputReader.RightClickEvent -= SetClickMovement;
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }
}
