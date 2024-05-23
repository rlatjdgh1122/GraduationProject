using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GifScreenPanel : PopupUI
{
    private TextMeshProUGUI _speedText;

    private GifScreenController _controller;

    private string _startText;

    public override void Awake()
    {
        base.Awake();

        _speedText = transform.Find("GifSpeedUp/Text").GetComponent<TextMeshProUGUI>();
        _controller = GetComponent<GifScreenController>();

        _startText = _speedText.text;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public void ResetPanel()
    {
        _speedText.text = _startText;
    }

    public void ChangeSpeedText()
    {
        _speedText.text = _controller.GifDataSO.GifSpeedList[_controller.SpeedUpBtnClickCount].SpeedText;
    }
}