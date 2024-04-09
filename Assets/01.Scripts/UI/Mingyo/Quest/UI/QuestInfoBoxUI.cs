using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoBoxUI : PoolableMono
{
    [SerializeField]
    private Slider _questValueSlider;

    [SerializeField]
    private TextMeshProUGUI _questGoalText, _questProgressText;

    [SerializeField]
    private Image _questTypeIMG;

    private float sliderMaxValue;

    public void SetUpQuestInfoBoxUI(float sliderValue,float sliderMaxValue, string text, Sprite Img)
    {
        _questValueSlider.maxValue = sliderMaxValue;
        _questGoalText.text = text;
        this.sliderMaxValue = sliderMaxValue;
        _questTypeIMG.sprite = Img;

        UpdateSliderValue(sliderValue);
    }

    public void UpdateSliderValue(float sliderValue)
    {
        _questValueSlider.value = sliderValue;
        _questProgressText.SetText($"{sliderValue} / {sliderMaxValue}");
    }
}
