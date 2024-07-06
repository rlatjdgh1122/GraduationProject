using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DeadDefenseBuilding : DeadBuilding
{
    [SerializeField] private float _disolveDuration;

    private MeshRenderer[] _visualRenderers;

    protected override void Awake()
    {
        base.Awake();

        _visualRenderers = transform.Find("Visual")?.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += BrokenBuilding;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= BrokenBuilding;
    }

    public override void BrokenBuilding()
    {
        StartDissolve();
    }

    #region Dissolve

    private void StartDissolve()
    {
        if (_visualRenderers != null)
        {
            foreach (var mat in _visualRenderers)
            {
                mat.material.DOFloat(-5, "_DissolveHeight", _disolveDuration);
            }

            ResetDisolveMat();

            StartCoroutine(DisolveCoroutine());
        }
        else
        {
            PushBuilding();
        }
    }

    private IEnumerator DisolveCoroutine()
    {
        yield return new WaitForSeconds(_disolveDuration + 1f);

        PushBuilding();
    }

    private void ResetDisolveMat()
    {
        foreach (var mat in _visualRenderers)
        {
            mat.material.SetFloat("_DissolveHeight", 7);
        }
    }

    #endregion
}