using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurningRope : MonoBehaviour
{
    private Material _ropeMat;

    private ParticleSystem _burningParticleSystem;

    private readonly float minRopeMatcutoffValue = 0.04f;
    private readonly float maxRopeMatcutoffValue = 0.62f;

    private float duration = 3f;
    public float Duration => duration; // 나중에 Set해서 바꿔주셈 함수 만들어놨음 이거 바꾸면 불꽃 튀는 이펙트 시간도 바꿔줘야 함

    private Animator _particleAnimator;

    private void Awake()
    {
        _ropeMat = GetComponent<MeshRenderer>().material;
        Transform effect = transform.Find("BurningEffect");
        _particleAnimator = effect.GetComponent<Animator>();
        _burningParticleSystem = effect.GetComponent<ParticleSystem>();
    }

    public void ReSetMatValue()
    {
        _ropeMat.SetFloat("_CutoffValue", maxRopeMatcutoffValue);
    }

    public void SetRopeBurningCorouHandler()
    {
        StartCoroutine(SetRopeBurning());
    }

    private IEnumerator SetRopeBurning()
    {
        float startTime = Time.time;

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            _ropeMat.SetFloat("_CutoffValue", Mathf.Lerp(maxRopeMatcutoffValue, minRopeMatcutoffValue, t));
            yield return null;
        }

        _ropeMat.SetFloat("_CutoffValue", minRopeMatcutoffValue);
        StopBurningParticle();
    }

    public void PlayBurningParticle()
    {
        _burningParticleSystem.Play();
        _particleAnimator.SetTrigger("BurningTrigger");
    }

    private void StopBurningParticle()
    {
        _burningParticleSystem.Stop();
    }

    public void SetFireDuration(float value)
    {
        duration = value;
    }
}
