using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    [Header("Input UI")]
    [SerializeField] private TextMeshProUGUI _currentCostText;
    [SerializeField] private TextMeshProUGUI _addCostText;
    [SerializeField] private Image _fishIcon;

    [Header("Max Repeat Count")]
    [SerializeField] private int _repeatCnt;

    [Header("Icon")]
    [SerializeField] private float _scaleValue;
    [SerializeField] private float _duration;



    private IEnumerator TweenCorou(int repeat)
    {
        for (int i = 0; i < repeat; i++)
        {
            Tween scaleTween = _fishIcon.rectTransform.DOScale(_scaleValue, _duration);
            yield return scaleTween.WaitForCompletion(); // Wait until the scaling up tween completes

            Tween scaleDownTween = _fishIcon.rectTransform.DOScale(1, _duration);
            yield return scaleDownTween.WaitForCompletion(); // Wait until the scaling down tween completes
        }
    }

    public void CostTween(int value, Transform startPosition)
    {
        int repeat = value;

        UIManager.Instance.InitializHudTextSequence();

        if (value >= _repeatCnt)
        {
            repeat = _repeatCnt;
        }

        StartCoroutine(TweenCorou(repeat));

        ChangeCost(value);
    }


    public void ChangeCost(int value)
    {
        UIManager.Instance.InitializHudTextSequence();

        Color plusColor = new Color(0, 255, 0); //임시
        Color minusColor = new Color(255, 0, 0); //임시
        string sign;
        
        if(value > 0)
        {
            sign = "+";
            _addCostText.color = plusColor;
        }
        else
        {
            sign = "";
            _addCostText.color = minusColor;
        }

        _addCostText.text = $"{sign}{value}";

        _addCostText.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                _currentCostText.text = $"{CostManager.Instance.Cost}";
            });
        
        _addCostText.alpha = 1;

    }

    public void OnlyCurrentCostView(int cost)
    {
        _currentCostText.text = $"{cost}";
    }
}