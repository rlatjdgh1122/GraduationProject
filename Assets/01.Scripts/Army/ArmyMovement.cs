using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private ParticleSystem ClickParticle;
    private Army curArmy => ArmyManager.Instance.GetCurArmy();
    private void Awake()
    {
        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        _inputReader.RightClickEvent += SetClickMovement;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            curArmy.AddStat(10,StatType.Damage,StatMode.Increase);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            curArmy.RemoveStat(10, StatType.Damage, StatMode.Increase);
        }
    }

    public void SetClickMovement()
    {
        if (/*curArmy.IsMoving
            && */curArmy.Soldiers.TrueForAll(s => s.NavAgent.enabled))
        {
            RaycastHit hit;

            if (Physics.Raycast(GameManager.Instance.RayPosition(), out hit))
            {
                SetArmyMovePostiton(hit.point);
                ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                ClickParticle.Play();
            }
        }
    }

    /// <summary>
    /// 배치대로 이동
    /// </summary>
    /// <param name="mousePos"> 마우스 위치</param>
    private void SetArmyMovePostiton(Vector3 mousePos)
    {
        var soldiers = curArmy.Soldiers;

        foreach (var soldier in soldiers)
        {
            soldier.MoveToMySeat(mousePos);
        }
    }

    private void OnDestroy()
    {
        _inputReader.RightClickEvent -= SetClickMovement;
    }
}
