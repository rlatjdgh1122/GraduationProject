using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextOuput : MonoBehaviour
{
    private ButtonEvent _btnEvent;
    [SerializeField] private TextMeshProUGUI _cntText;
    [SerializeField] private Image _img;

    private float _curTime;
    private float _maxTime;

    private void Awake()
    {
        _btnEvent = GetComponent<ButtonEvent>();
        _btnEvent.OnCurCountChangedEvent += HandleValueChanged;
        _curTime = _btnEvent.CoolTime;
        _maxTime = _btnEvent.CoolTime;
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
        if(!_btnEvent.CanSpawn)
        {
            _curTime -= Time.deltaTime;
            _img.fillAmount = _curTime/ _maxTime;
        }
        else
        {
            _curTime = _maxTime;
        }
    }
}