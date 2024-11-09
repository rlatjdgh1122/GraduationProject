using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class SkillUI : MonoBehaviour
{
    [SerializeField] protected UnityEvent OnSkillReadyEvent = null;
    protected Image bliendGauge = null;

    protected int value = 0;
    public float CurrntValue = 0f;
    public float CurrentFillAmount = 0;

    protected virtual void Awake()
    {
        
    }

    private void OnEnable()
    {
        bliendGauge = transform.Find("BliendGauge").GetComponent<Image>();
    }

    public void OnChangedMaxValue(int maxValue)
    {
        value = maxValue;
    }

    public void Init()
    {

    }

    public virtual void OnSkillUsed()
    {
        try
        {
            bliendGauge.fillAmount = 1f;
            CurrentFillAmount = 1f;
        }
        catch(MissingReferenceException ex)
        {
            Debug.Log(gameObject.name + " " + gameObject.GetInstanceID());
        }
       
       
    }

    public virtual void OnSkillActionEnter() { }

    public virtual void OnSkillReady()
    {
        OnSkillReadyEvent?.Invoke();
    }
}
