using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanceSkill : Skill
{
    [SerializeField] private float _prickDelay;

    private Coroutine _prickCoroutine;
    public bool canPrick = false;

    public UnityEvent OnPrickEvent;
    public UnityEvent OnChargingEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        Prick(_prickDelay);
    }

    public void Prick(float delay)
    {
        OnChargingEvent?.Invoke();

        if (_prickCoroutine != null)
            StopCoroutine(PrickCoroutine(0));

        _prickCoroutine = StartCoroutine(PrickCoroutine(delay));
    }

    private IEnumerator PrickCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        OnPrickEvent?.Invoke();
    }
}
