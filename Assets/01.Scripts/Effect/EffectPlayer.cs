using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : PoolableMono
{
    [SerializeField]
    private List<ParticleSystem> _particles;
    public List<ParticleSystem> Particles => _particles;

    public void StartPlay(float endTime)
    {
        if (_particles != null)
            _particles.ForEach(p => p.Play());

        StartCoroutine(Timer(endTime));
    }

    protected IEnumerator Timer(float timer)
    {
        yield return new WaitForSeconds(timer);
        PoolManager.Instance.Push(this);
    }
}
