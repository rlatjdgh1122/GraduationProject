using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextOuput : MonoBehaviour
{
    private BtnEvent _btnEvent;
    public TextMeshProUGUI[] _cntText;
    public Image[] _img;

    private float _curTime;
    private float _maxTime;

    private void Awake()
    {
        _btnEvent = GetComponent<BtnEvent>();
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
        for(int i = 0; i < _cntText.Length; i++)
        {
            _cntText[i].text = $"{newCnt} / {maxCount}";
        }

    }

    private void Update()
    {
        if(!_btnEvent.CanSpawn)
        {
            _curTime -= Time.deltaTime;
            for(int i = 0; i < _cntText.Length; i++)
            {
                _img[i].fillAmount = _curTime/ _maxTime;
            }
        }
        else
        {
            _curTime = _maxTime;
        }
    }
}