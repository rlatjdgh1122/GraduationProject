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
    private TextMeshProUGUI _questGoalText, _rewardCount;

    [SerializeField]
    private Image _questTypeIMG;

    private float sliderMaxValue;

    public void SetUpQuestInfoBoxUI(float sliderValue,float sliderMaxValue, string text, Sprite Img)
    {
        _questValueSlider.maxValue = sliderMaxValue;
        UpdateSliderValue(sliderValue);
        _questGoalText.text = text;
        this.sliderMaxValue = sliderMaxValue;
        _questTypeIMG.sprite = Img;
    }

    public void UpdateSliderValue(float sliderValue)
    {
        _questValueSlider.value = sliderValue;
        _rewardCount.SetText($"{sliderValue} / {sliderMaxValue}");
    }
}
