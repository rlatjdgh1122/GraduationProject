using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField]
    private float _disolveDuration;

    private MeshRenderer _visualRenderer;
    private Material _disolveMat;

    private bool _isClick = false;

    private void Awake()
    {
        _visualRenderer = transform.Find("Visual").GetComponent<MeshRenderer>();
        _disolveMat = _visualRenderer.material; ;
    }

    private void OnMouseDown()
    {
        if (!_isClick && !WaveManager.Instance.IsBattlePhase)
        {
            _visualRenderer.material.DOFloat(0, "_DissolveHeight", _disolveDuration);
        }

        _isClick = true;
    }

    private void Disolve()
    {
    }
}
