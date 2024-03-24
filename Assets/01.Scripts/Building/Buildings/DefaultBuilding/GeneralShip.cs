using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralShip : MonoBehaviour
{
    [SerializeField] private Transform innerTrm;
    [SerializeField] private Transform outTrm;

    bool isArrived = false;

    private void Awake()
    {
        SignalHub.OnBattlePhaseStartEvent += MoveShip;
        SignalHub.OnBattlePhaseEndEvent += ReturnShip;
    }

    private void Start()
    {
        ReturnShip();
    }

    private void MoveShip()
    {
        transform.DOMove(outTrm.position, 12f);
        transform.DOLookAt(outTrm.transform.position, 2f);
    }

    private void ReturnShip()
    {
        transform.DOMove(innerTrm.position, 10f);
        transform.DOLookAt(innerTrm.transform.position, 2f);
    }

    private void OnMouseDown()
    {
        UIManager.Instance.ShowPanel("GeneralUI");
    }
}
