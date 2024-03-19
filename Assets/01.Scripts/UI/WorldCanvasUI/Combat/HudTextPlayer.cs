using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudTextPlayer : PoolableMono
{
    [SerializeField]
    private EvasionHudTextUI hudTextUI;

    public void StartPlay(float endTime)
    {
        hudTextUI.ShowUI();

        StartCoroutine(Timer(endTime));
    }

    protected IEnumerator Timer(float timer)
    {
        yield return new WaitForSeconds(timer);
        PoolManager.Instance.Push(this);
    }
}
