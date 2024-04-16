using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurningRope : MonoBehaviour
{
    private Material _ropeMat;

    private ParticleSystem _burningSystem;

    private readonly float minRopeMatcutoffValue = 4.35f;
    private readonly float maxRopeMatcutoffValue = 4.75f;

    private float duration = 3f;
    public float Duration => duration; // 나중에 Set해서 바꿔주셈 함수 만들어놨음

    private void Awake()
    {
        _ropeMat = GetComponent<MeshRenderer>().material;
        _burningSystem = transform.Find("BurningEffect").GetComponent<ParticleSystem>();
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

        while (Time.time - startTime < duration)
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
        _burningSystem.Play();
    }

    private void StopBurningParticle()
    {
        _burningSystem.Stop();
    }

    public void SetFireDuration(float value)
    {
        duration = value;
    }
}
