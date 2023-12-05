using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextOuput : MonoBehaviour
{
    private ButtonEvent _btnEvent;
    [SerializeField] private TextMeshProUGUI _cntText;
    [SerializeField] private TextMeshProUGUI _completText;

    private void Awake()
    {
        _btnEvent = GetComponent<ButtonEvent>();
        _btnEvent.OnCurCountChangedEvent += HandleValueChanged;

    }

    private void OnDestroy()
    {

        _btnEvent.OnCurCountChangedEvent -= HandleValueChanged;

    }

    private void HandleValueChanged(int newCnt, int maxCount)
    {

        _cntText.text = $"{newCnt} / {maxCount}";

    }

    private void Update()
    {        
        if(_btnEvent.CanSpawn)
        {
            _completText.text = "스폰 가능";
        }
        else
        {
            _completText.tag = "기다려!";
        }
    }
}
