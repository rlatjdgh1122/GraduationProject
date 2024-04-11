using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedUpButtonUI : MonoBehaviour
{
    private int clickCount = 0;
    private float[] TimeScale = new float[] { 2f, 0f, 1f };
    private string[] Text = new string[] { "일시 정지", "게임 재생", "x2 재생 속도"};

    private TextMeshProUGUI _txt = null;
    private Button _btn = null;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _txt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _btn.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        float time = TimeScale[clickCount];
        string text = Text[clickCount];

        _txt.text = text;
        Time.timeScale = time;

        clickCount = ++clickCount >= 3 ? 0 : clickCount;
    }
}
