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
    private TextMeshProUGUI _questGoalText;

    [SerializeField]
    private Image _questTypeIMG;

    public void SetUpQuestInfoBoxUI(float value, string text, Sprite Img)
    {
        _questValueSlider.value = value;
        _questGoalText.text = text;
        _questTypeIMG.sprite = Img;
    }
}
