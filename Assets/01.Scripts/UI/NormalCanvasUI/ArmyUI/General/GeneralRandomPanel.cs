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
    [SerializeField] private GameObject _iconSlot;
    [SerializeField] private GameObject _questionMark;

    [Header("Components")]
    [SerializeField] private GeneralRandomPanelEffect _effect;
    [SerializeField] private GeneralRandomResultPanel _resultPanel;

    public void StartGamble()
    {
        ShowPanel();
        MoveSlot();
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
        _rect.anchoredPosition = new Vector2(0, 3f);
        _iconSlot.SetActive(false);
        _questionMark.SetActive(false);

        _resultPanel.ShowPanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        _iconSlot.SetActive(true);
        _questionMark.SetActive(true);
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
