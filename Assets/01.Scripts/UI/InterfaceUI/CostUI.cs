using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour  
{
    [Header("Input UI")]
    [SerializeField] private TextMeshProUGUI _currentCostText;
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
        ChangeCost();
    }

    #region ParticleImage


    public void CostArriveText(float repeat)
    {
        _currentCostText.text = $"{repeat}";
    }

    public void CostStopText()
    {
        _currentCostText.text = $"{CostManager.Instance.Cost}";
    }
    #endregion

    public void ChangeCost()
    {
        _currentCostText.text = $"{CostManager.Instance.Cost}";
    }

    public void OnlyCurrentCostView(float cost)
    {
        _currentCostText.text = $"{cost}";
    }
}