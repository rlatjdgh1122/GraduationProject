using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseUI : MonoBehaviour
{
    [SerializeField]
    private float _fadeNoiseUIDuration;

    [SerializeField]
    private float _fillamountFadeDuration;

    private Image _noiseFillImage;
    private CanvasGroup _canvasGroup;
    private NoiseManager _noiseManager => NoiseManager.Instance;

    private void Awake()
    {
        Transform trm = transform.Find("MainPanel/FillAmountPanel");
        _noiseFillImage = trm.Find("FillAmount").GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>(); 
    }

    private void OnEnable()
    {
        NoiseManager.Instance.NoiseIncreaseEvent += IncreaseNoiseUI;
        SignalHub.OnBattlePhaseStartEvent += HideNoiseUI;
        SignalHub.OnBattlePhaseEndEvent += ShowNoiseUI;
    }

    private void OnDisable()
    {
        NoiseManager.Instance.NoiseIncreaseEvent -= IncreaseNoiseUI;
        SignalHub.OnBattlePhaseStartEvent -= HideNoiseUI;
        SignalHub.OnBattlePhaseEndEvent -= ShowNoiseUI;
    }

    private void IncreaseNoiseUI()
    {
        float noiseValue = _noiseManager.CurrentNoise / _noiseManager.MaxNoise;

        _noiseFillImage.DOFillAmount(noiseValue, _fillamountFadeDuration);
    }

    public void ShowNoiseUI()
    {
        _canvasGroup.DOFade(1, _fadeNoiseUIDuration);
    }

    public void HideNoiseUI()
    {
        _canvasGroup.DOFade(0, _fadeNoiseUIDuration).OnComplete(() => ResetUI());
    }

    public void ResetUI()
    {
        _noiseManager.ResetNoise();
        _noiseFillImage.fillAmount = 0;
    }
}
