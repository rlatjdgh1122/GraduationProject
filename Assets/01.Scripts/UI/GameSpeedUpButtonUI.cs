using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedUpButtonUI : MonoBehaviour
{
    private Button _btn = null;
    private TextMeshProUGUI _txt = null;
    private bool isSpeedUp = false;

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
        isSpeedUp = !isSpeedUp;

        float timeScale = isSpeedUp ? 2f : 1f;
        string text = isSpeedUp ? "일시 정지" : "x2 재생 속도";
        _txt.text = text;
        Time.timeScale = timeScale;


    }
}
