using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GifViewButton : MonoBehaviour
{
    private GifType _gifType;
    private TextMeshProUGUI _text;
    private Button _button;

    private void Awake()
    {
        _button = transform.Find("Button").GetComponent<Button>();
        _text = transform.Find("VideoName").GetComponent<TextMeshProUGUI>();

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(BtnClickEvent);
    }

    public void Init(ScreenData data)
    {
        if (data == null) return;

        _gifType = data.GifType;
        _text.text = data.GifName;
    }

    public void BtnClickEvent()
    {
        UIManager.Instance.GifController.ShowGif(_gifType);
    }
}