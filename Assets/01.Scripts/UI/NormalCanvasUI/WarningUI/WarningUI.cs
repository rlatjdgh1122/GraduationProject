using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningUI : PopupUI
{
    private TextMeshProUGUI _text;

    public float IntervalTime = 1.0f;

    public override void Awake()
    {
        base.Awake();

        _text = transform.Find("_WarningText").GetComponent<TextMeshProUGUI>();
    }

    public void SetValue(string text)
    {
        _rectTransform.position = new Vector3
            (_rectTransform.transform.position.x, _rectTransform.transform.position.y, _rectTransform.transform.position.y);
        _text.text = text;
    }

    public override void ShowAndHidePanel(float waitTime)
    {
        base.ShowAndHidePanel(waitTime);
    }
}
