using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KatanaSkill : Skill
{
    [SerializeField] private float _dashDelay;

    private Coroutine _dashCoroutine;
    public bool canDash = false;

    public UnityEvent OnSlashEvent;
    public UnityEvent OnDashEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        Dash(_dashDelay);

        StartCoroutine(Sound());
    }

    public void Dash(float delay)
    {
        OnSlashEvent?.Invoke();

        if (_dashCoroutine != null)
            StopCoroutine(DashCoroutine(0));

        _dashCoroutine = StartCoroutine(DashCoroutine(delay));
    }

    private IEnumerator DashCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        OnDashEvent?.Invoke();
    }

    private IEnumerator Sound()
    {
        for (int i = 0; i < 16; i++)
        {
            SoundManager.Play3DSound(SoundName.SkillKatana, transform.position);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);
        SoundManager.Play3DSound(SoundName.SkillEndKatana, transform.position);
    }
}