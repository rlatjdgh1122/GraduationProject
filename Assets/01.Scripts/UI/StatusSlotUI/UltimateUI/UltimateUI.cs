using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UltimateUI : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnUltimateReadyEvent = null;
    protected Image bliendGauge = null;

    protected int value = 0;
    protected float currentFillAmount = 0;

    protected virtual void Awake()
    {
        bliendGauge = transform.Find("BliendGauge").GetComponent<Image>();
    }

    public void OnChangedMaxValue(int maxValue)
    {
        value = maxValue;
    }

    public void Init()
    {
        bliendGauge.fillAmount = 1f;
        currentFillAmount = 1f;
    }

    public virtual void OnUltimateUsed()
    {
        Init();
    }

    public virtual void OnUltimateActionEnter() { }
}
