using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralInfoPanel : PopupUI
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Slider _defSlider;
    [SerializeField] private Slider _atkSlider;
    [SerializeField] private Slider _rngSlider;
    [SerializeField] private TextMeshProUGUI _priceText;

    public override void Awake()
    {
        base.Awake();
    }

    public void SetElements(GeneralStat general)
    {
        _descriptionText.text = general.InfoData.Description;
        _defSlider.DOValue(general.InfoData.hp, 0.5f);
        _atkSlider.DOValue(general.InfoData.atk, 0.5f);
        _rngSlider.DOValue(general.InfoData.range, 0.5f);
        _priceText.text = $"{general.InfoData.Price}";
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
