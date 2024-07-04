using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UltimateUI : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnUltimateReadyEvent = null;
    protected Image bliendGauge = null;

    public int Value = 0;
    public float CurrentFillAmount = 0;

    protected virtual void Awake()
    {
        bliendGauge = transform.Find("BliendGauge").GetComponent<Image>();
    }

    public void OnChangedMaxValue(int maxValue)
    {
        Value = maxValue;
    }

    public void Init()
    {

    }

    public virtual void OnUltimateUsed()
    {
        bliendGauge.fillAmount = 1f;
        CurrentFillAmount = 1f;
    }

    public virtual void OnUltimateActionEnter() { }
}
