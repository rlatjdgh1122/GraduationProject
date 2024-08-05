using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingTimerUI : WorldUI
{
    [SerializeField] private Image _gauge;

    private Coroutine _coroutine = null;

    public override void Awake()
    {
        base.Awake();

        canvas.alpha = 0f;
    }

    public void StartTimer(float healingTime)
    {
        if (_coroutine != null)
        {
            StopCoroutine(StartHealingTimerCorou(healingTime));
        }
        _coroutine = StartCoroutine(StartHealingTimerCorou(healingTime));

        _gauge.fillAmount = 0;
        ShowUI();
    }

    public void BrokenBuilding()
    {
        _gauge.fillAmount = 0;
        FadeOutImmediately();
    }

    private IEnumerator StartHealingTimerCorou(float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            _gauge.fillAmount = Mathf.Clamp01(elapsedTime / time);
            yield return null;
        }

        FadeOutImmediately();
    }



}
