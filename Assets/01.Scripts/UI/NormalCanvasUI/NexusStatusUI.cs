using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NexusStatusUI : PopupUI
{
    private Image _fill;

    private float _current => NexusManager.Instance.NexusBase.HealthCompo.currentHealth;
    private float _max => NexusManager.Instance.NexusBase.HealthCompo.maxHealth;

    public override void Awake()
    {
        base.Awake();


        _fill = transform.Find("Fill").GetComponent<Image>();

        SignalHub.OnBattlePhaseStartEvent += ShowPanel;
        SignalHub.OnBattlePhaseEndEvent += HidePanel;
    }

    private void Start()
    {
        if (NexusManager.Instance == null)
        {
            Debug.LogError("NexusManager.Instance is null");
            return;
        }

        if (NexusManager.Instance.NexusBase == null)
        {
            Debug.LogError("NexusManager.Instance.NexusBase is null");
            return;
        }

        if (NexusManager.Instance.NexusBase.HealthCompo == null)
        {
            Debug.LogError("NexusManager.Instance.NexusBase.HealthCompo is null");
            return;
        }
    }


    public override void HidePanel()
    {
        base.HidePanel();
        NexusManager.Instance.NexusBase.HealthCompo.OnHit -= FillAmount;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        NexusManager.Instance.NexusBase.HealthCompo.OnHit += FillAmount;
        FillAmount();
    }

    public void FillAmount()
    {
        _fill.DOFillAmount(_current / _max, 0.5f);
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= ShowPanel;
        SignalHub.OnBattlePhaseEndEvent -= HidePanel;
    }
}
