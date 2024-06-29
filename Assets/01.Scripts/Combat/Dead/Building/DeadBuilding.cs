using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBuilding : MonoBehaviour
{
    [SerializeField]
    private float _disolveDuration;

    private MeshRenderer[] _visualRenderers;
    private BaseBuilding _parentBuilding;
    public BaseBuilding ParentBuilding => _parentBuilding;

    private void Awake()
    {
        _parentBuilding = transform.parent.GetComponent<BaseBuilding>();
        _visualRenderers = transform.Find("Visual")?.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += DisolveBuilding;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= DisolveBuilding;
    }

    public virtual void DisolveBuilding()
    {
        if(_visualRenderers != null)
        {
            foreach (var mat in _visualRenderers)
            {
                mat.material.DOFloat(-5, "_DissolveHeight", _disolveDuration);
            }

            ResetDisolveMat();
            
            StartCoroutine(DisolveCoroutine());
        }

        PoolManager.Instance.Push(_parentBuilding);
    }

    private IEnumerator DisolveCoroutine()
    {
        yield return new WaitForSeconds(_disolveDuration + 1f);
        PoolManager.Instance.Push(_parentBuilding);
    }

    private void ResetDisolveMat()
    {
        foreach (var mat in _visualRenderers)
        {
            mat.material.SetFloat("_DissolveHeight", 7);
        }
    }
}