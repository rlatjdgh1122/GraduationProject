using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour  
{
    [Header("Input UI")]
    [SerializeField] private TextMeshProUGUI _currentCostText;
    [SerializeField] private TextMeshProUGUI _addCostText;
    [SerializeField] private Image _fishIcon;
    [SerializeField] private CostParticle _costParticleImage;

    [Header("Cost")]
    [SerializeField] private int _maxCostCount;

    private int _cost;
    private int _divideCost;

    private CostParticle _particleImage;

    public void CostTween(int value, bool isUI, Vector3 startPosition)
    {
        if (value <= 0)
        {
            return;
        }
        _particleImage = PoolManager.Instance.Pop(_costParticleImage.name) as CostParticle;

        _particleImage.TargetPosition(transform, _fishIcon);

        _cost = value;
        if (value > _maxCostCount)
        {
            value = _maxCostCount;
        }

        _divideCost = _cost / value;

        _particleImage.SetBurst(0, 0, value);
        _particleImage.Setting(_cost, _divideCost);

        if (!isUI)
        {
            startPosition = Camera.main.WorldToScreenPoint(startPosition);
        }
        _particleImage.Position(startPosition);
        _particleImage.PlayParticle();
        AddCost(_cost);
    }

    #region ParticleImage


    public void CostArriveText(int repeat)
    {
        _currentCostText.text = $"{repeat}";
    }

    public void CostStopText()
    {
        _currentCostText.text = $"{CostManager.Instance.Cost}";
    }
    #endregion


    public void AddCost(int value)
    {
        Color plusColor = new Color(0, 255, 0); //임시
        _addCostText.color = plusColor;
        _addCostText.text = $"+{value}";

        ChangeCost();
    }
    public void SubtractCost(int value)
    {
        Color minusColor = new Color(255, 0, 0); //임시
        _addCostText.color = minusColor;
        _addCostText.text = $"{value}";

        ChangeCost();
    }
    public void ChangeCost()
    {
        _addCostText.alpha = 1;

        _addCostText.DOFade(0, 0.6f)
            .OnComplete(() =>
            {
                _currentCostText.text = $"{CostManager.Instance.Cost}";
            });
    }

    public void OnlyCurrentCostView(int cost)
    {
        _currentCostText.text = $"{cost}";
    }
}