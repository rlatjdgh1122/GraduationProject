using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralShip : MonoBehaviour
{
    [SerializeField] private Transform innerTrm;
    [SerializeField] private Transform outTrm;

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

    private bool isFirst = true;
    private void OnMouseDown()
    {
        Debug.Log($"GeneralShip Debug) IsBattlePhase {WaveManager.Instance.IsBattlePhase}, " +
            $"Gif {UIManager.Instance.GifController.CanShow}, " +
            $"Legion {LegionInventoryManager.Instance.CanUI}, Nexus {NexusManager.Instance.CanClick}");
        if(!WaveManager.Instance.IsBattlePhase 
            && !UIManager.Instance.GifController.CanShow
            && !LegionInventoryManager.Instance.CanUI
            && !NexusManager.Instance.CanClick)
        {
            if (WaveManager.Instance.CurrentWaveCount <= 4)
            {
                UIManager.Instance.ShowWarningUI("튜토리얼이 진행되지 않았습니다");
                return;
            }

            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
            UIManager.Instance.ShowPanel("GeneralUI");

            if (WaveManager.Instance.CurrentWaveCount == 5)
            {
                if (isFirst)
                {
                    UIManager.Instance.GifController.ShowGif(GifType.GeneralBuy);
                    isFirst = false;
                }
            }
        }
    }
}
