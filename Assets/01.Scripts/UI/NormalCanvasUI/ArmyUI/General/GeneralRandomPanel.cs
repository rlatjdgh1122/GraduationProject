using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralRandomPanel : ArmyComponentUI
{
    [Header("Objects")]
    [SerializeField] private RectTransform _rect;
    [SerializeField] private ParticleImage _particle;
    [SerializeField] private ParticleImage _smallParticle;
    [SerializeField] private GeneralRandomPanelEffect _effect;
    [SerializeField] private GameObject _iconSlot;
    [SerializeField] private GameObject _questionMark;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            ShowPanel();
            MoveSlot();
        }
    }

    public void MoveSlot()
    {
        _effect.ShowPanel();
        _rect.DOAnchorPosX(-4900, 3f).SetEase(Ease.InSine).OnComplete(() => CompleteGameble());
    }

    public void CompleteGameble()
    {
        _particle.Play();
        _smallParticle.Play();
        _iconSlot.SetActive(false);
        _questionMark.SetActive(false);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
