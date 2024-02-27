using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUpgradeUI : MonoBehaviour
{
    public PenguinStat GeneralStat;

    #region components
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _nameText;
    private Slider _atkBox;
    private Slider _defBox;
    private Slider _rangeBox;
    private TextMeshProUGUI _levelText;
    private TextMeshProUGUI _priceText;
    #endregion

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _nameText = transform.Find("GeneralInfo/name").GetComponent<TextMeshProUGUI>();
        _atkBox = transform.Find("GeneralInfo/_atk").GetComponent<Slider>();
        _defBox = transform.Find("GeneralInfo/_def").GetComponent<Slider>();
        _rangeBox = transform.Find("GeneralInfo/_range").GetComponent<Slider>();
        _levelText = transform.Find("GeneralInfo/level").GetComponent<TextMeshProUGUI>();
        _priceText = transform.Find("GeneralInfo/PurchaseButton/text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTexts()
    {
        _levelText.text = $"LV {GeneralStat.PenguinData.level}";
        _priceText.text = $"LV {GeneralStat.PenguinData.level} -> LV {GeneralStat.PenguinData.level + 1}  {GeneralStat.PenguinData.levelUpPrice}";
        GeneralStat.UpdateAblitiyUI(_nameText, _atkBox, _defBox, _rangeBox);
    }

    public void OpenPanel(PenguinStat stat)
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        GeneralStat = stat;
        UpdateTexts();
        _canvasGroup.DOFade(1, 0.4f);
    }

    public void PanelOff()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.DOFade(0, 0.4f);
    }
}
