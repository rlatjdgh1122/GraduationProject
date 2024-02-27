using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralInfoUI : MonoBehaviour
{
    public PenguinStat GeneralStat;

    private CanvasGroup _canvasGroup;
    private Slider _atkBox;
    private Slider _defBox;
    private Slider _rangeBox;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _atkBox = transform.Find("GeneralInfo/_atk").GetComponent<Slider>();
        _defBox = transform.Find("GeneralInfo/_def").GetComponent<Slider>();
        _rangeBox = transform.Find("GeneralInfo/_range").GetComponent<Slider>();

        UpdateTexts();
    }

    public void UpdateTexts()
    {
        GeneralStat.UpdateAblitiyUI(_atkBox, _defBox, _rangeBox);
    }

    public void OpenPanel(PenguinStat stat)
    {
        GeneralStat = stat;
        _canvasGroup.DOFade(1, 0.4f);
    }

    public void PanelOff()
    {
        _canvasGroup.DOFade(0, 0.4f);
    }
}
