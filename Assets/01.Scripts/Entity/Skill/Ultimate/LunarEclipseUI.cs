using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LunarEclipseUI : MonoBehaviour
{
    [SerializeField]
    private Color _beforeGlow;

    [SerializeField]
    private Color _afterGlow = Color.red;

    [SerializeField]
    private float _localMoveXValue;

    [SerializeField] private Image _glowImge;
    [SerializeField] private Image _redMoonImage;

    [SerializeField] private float _duration;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
            StartEclipse(_duration);
    }

    public void StartEclipse(float duration)
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence
            .Append(_canvasGroup.DOFade(1, 0.7f))
            .Join(_redMoonImage.DOFade(1, 0.5f))
            .Join(_redMoonImage.transform.DOLocalMoveX(0, duration))
            .Join(_redMoonImage.DOColor(Color.white, 7f))
            .Join(_glowImge.DOColor(_afterGlow, duration))
            .AppendInterval(2.3f) //나중에 이부분 지워야함
            .AppendCallback(() => EndEclipse(duration));
    }

    public void EndEclipse(float duration)
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence
            .Append(_redMoonImage.DOFade(0, duration))
            .Join(_redMoonImage.transform.DOLocalMoveX(_localMoveXValue, duration))
            .Join(_glowImge.DOColor(_beforeGlow, duration))
            .Join(_redMoonImage.DOColor(Color.black, 7f))
            .Join(_canvasGroup.DOFade(0, duration))
            .Append(_redMoonImage.transform.DOLocalMoveX(-_localMoveXValue,0)); //다시 원래 자리로 옮기기
    }
}
