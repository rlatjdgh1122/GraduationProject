using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BombTimerSliderUI : WorldUI
{
    [SerializeField] private Image _timerImage; 
    [SerializeField] private float duration = 10f;

    EnemyBombPenguin _penguin;

    public override void Update()
    {
        base.Update();

        try
        {
            if (_penguin.IsDead)
            {
                Debug.Log("¾Ó");
                FadeOutImmediately();
                return;
            }
            if (_timerImage.fillAmount >= 1)
            {
                FadeOutImmediately();
                return;
            }
        }
        catch
        {
        }
    }

    public void UpdateBombSliderUI()
    {
        canvas.DOFade(1, 0.5f);
        _timerImage.DOFillAmount(1f, duration).SetEase(Ease.Linear);
    }

    public void FadeOutImmediately()
    {
        /*UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence.Append(canvas.DOFade(0, 0.3f));*/

        canvas?.DOKill();
        canvas.DOFade(0, 0.3f);
    }
}