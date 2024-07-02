using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class SkillUI : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnSkillReadyEvent = null;
    protected Image blinedGauge = null;

    protected int value = 0;
    protected float currentFillAmount = 0;

    protected virtual void Awake()
    {
        blinedGauge = transform.Find("BlinedGauge").GetComponent<Image>();
    }

    public void OnChangedMaxValue(int maxValue)
    {
        value = maxValue;
    }

    public void Init()
    {
        blinedGauge.fillAmount = 1f;
        currentFillAmount = 1f;
    }

    public virtual void OnSkillUsed()
    {
        Init();
    }

    public virtual void OnSkillActionEnter() { }
}
