using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseUI : MonoBehaviour
{
    [SerializeField]
    private float _duration;
    private Image _noiseFillImage;

    private void Awake()
    {
        Transform trm = transform.Find("background");
        _noiseFillImage = trm.Find("fillAmount").GetComponent<Image>();
    }

    public void IncreaseNoiseUI(float value)
    {
        _noiseFillImage.DOFillAmount(value, _duration);
    }

    public void ShowNoiseUI()
    {

    }

    public void HideNoiseUI()
    {

    }

    public void ResetUI()
    {
        _noiseFillImage.fillAmount = 0;
    }
}
