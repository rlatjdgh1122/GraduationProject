using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType
{
    Basic,
    Golden,
}

[Serializable]
public class BoxEvent
{
    [field:SerializeField] public BoxType Type { get; set; }

    #region GoldenBox
    [HideInInspector] public Transform Lid;
    [HideInInspector] public float OpenLidAngle;
    [HideInInspector] public float Duration;
    #endregion
}

public class CostBox : PoolableMono
{
    [SerializeField] private int _cost;
    [SerializeField] private ParticleSystem _clickParticle;
    [SerializeField] private Transform _box;

    public BoxEvent InspectorCustomBox;

    private void OnMouseDown()
    {
        switch (InspectorCustomBox.Type)
        {
            case BoxType.Golden:
            {
                UIManager.Instance.InitializHudTextSequence();
                UIManager.Instance.HudTextSequence.Append(
                InspectorCustomBox.Lid.DOLocalRotate(
                    new Vector3(InspectorCustomBox.OpenLidAngle, 0, 0), InspectorCustomBox.Duration))
                    .AppendInterval(0.5f) //ÀÓ½Ã °ª
                    .AppendCallback(() =>
                    {
                        ClickEvent();
                    });
            }
            break;

            default:
            {
                ClickEvent();
            }
            break;
        }        
    }

    private void ClickEvent()
    {
        _box.gameObject.SetActive(false);

        _clickParticle.Play();

        StartCoroutine(DisableBox());
    }

    private IEnumerator DisableBox()
    {
        yield return new WaitForSeconds(_clickParticle.duration);

        CostManager.Instance.AddFromCurrentCost(_cost, true, transform);

        PoolManager.Instance.Push(this);
    }
}
