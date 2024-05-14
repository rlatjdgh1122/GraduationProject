using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBuillding : MonoBehaviour
{
    [SerializeField]
    private float _disolveDuration;

    private MeshRenderer[] _visualRenderers;
    private BaseBuilding _parentBuilding;

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

    public void DisolveBuilding()
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

        Destroy(_parentBuilding.gameObject);
    }

    private IEnumerator DisolveCoroutine()
    {
        yield return new WaitForSeconds(_disolveDuration + 1f); 
        
        Destroy(_parentBuilding.gameObject);
    }

    private void ResetDisolveMat()
    {
        foreach (var mat in _visualRenderers)
        {
            mat.material.SetFloat("_DissolveHeight", 7);
        }
    }
}