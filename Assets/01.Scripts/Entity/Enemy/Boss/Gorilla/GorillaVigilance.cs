using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaVigilance : Vigilance
{
    [SerializeField] private float _sizeUpValue = 0.5f;
    [SerializeField] private float _sizeUpDuration = 0.5f;
    private Vector3 _defaultSize = Vector3.one;

    public override void SetTarget(Target target)
    {
        base.SetTarget(target);

        _defaultSize = target.transform.localScale;
    }
    public override void OnVigilance()
    {
        base.OnVigilance();

        Vector3 targetSize = (_target.transform.localScale) * _sizeUpValue;
        _target.transform.DOScale(targetSize, _sizeUpDuration);
    }

    public override void InitVigilance()
    {
        _target.transform.DOScale(_defaultSize, _sizeUpDuration);
    }
}
