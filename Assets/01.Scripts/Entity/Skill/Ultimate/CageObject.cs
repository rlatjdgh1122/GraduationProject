using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageObject : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 6f;
    [SerializeField] private float _disolveDuration = 2f;

    private MeshRenderer _visualRenderer;

    private void OnEnable()
    {
        _visualRenderer = GetComponent<MeshRenderer>();

        CoroutineUtil.CallWaitForSeconds(_destroyTime, () => SetDestroy());
    }

    private void SetDestroy()
    {
        StartCoroutine(DissolveCoroutine());
    }

    private IEnumerator DissolveCoroutine() //값 일단 임시
    {
        _visualRenderer.material.DOFloat(-5, "_DissolveHeight", _disolveDuration);

        yield return new WaitForSeconds(0.5f);

        Destroy(this);
    }
}
