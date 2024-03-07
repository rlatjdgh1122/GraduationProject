using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralInfoUI : MonoBehaviour
{
    public PenguinStat GeneralStat;

    [SerializeField] private List<GeneralPresenter> _generalUIList;

    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _nameText;
    private Slider _atkBox;
    private Slider _defBox;
    private Slider _rangeBox;
    private TextMeshProUGUI _priceText;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _nameText = transform.Find("GeneralInfo/name").GetComponent<TextMeshProUGUI>();
        _atkBox = transform.Find("GeneralInfo/_atk").GetComponent<Slider>();
        _defBox = transform.Find("GeneralInfo/_def").GetComponent<Slider>();
        _rangeBox = transform.Find("GeneralInfo/_range").GetComponent<Slider>();
        _priceText = transform.Find("GeneralInfo/PurchaseButton/text").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTexts()
    {
        _priceText.text = $"¿µÀÔ  {GeneralStat.PenguinData.price}";
        //GeneralStat.UpdateAblitiyUI(_nameText, _atkBox, _defBox, _rangeBox);
    }

    public void Purchase()
    {
        //if (CostManager.Instance.Cost >= GeneralStat.PenguinData.price)
        //{
        //    foreach (GeneralMainUI generalUI in _generalUIList)
        //    {
        //        if (generalUI.GeneralStat == GeneralStat)
        //        {
        //            generalUI.SetUpgradeUI();
        //            PanelOff();
        //            break;
        //        }
        //    }
        //}
    }

    public void OpenPanel(GeneralPresenter generalUI)
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        //GeneralStat = generalUI.GeneralStat;
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
