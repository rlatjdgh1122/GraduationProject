using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenguinStoreUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _buyPanel;
    [SerializeField] private float _fadeTime;
    [SerializeField] private int _maxCount;
    public int _cnt;

    #region BuyPanel

    public void OnEnableBuyPanel()
    {
        _buyPanel.DOFade(1, _fadeTime);
        _buyPanel.blocksRaycasts = true;
    }

    public void OnDisableBuyPanel()
    {
        _buyPanel.DOFade(0, _fadeTime);
        _buyPanel.blocksRaycasts = false;
    }

    #endregion

    #region BuyPanel Inner

    public void ChangeCount(string number)
    {
        _cnt = Int32.Parse(number);
    }

    public void PlusCnt()
    {
        if (_maxCount < _cnt) return;

        _cnt++;
    }

    public void MinusCnt()
    {
        if (_cnt <= 0) return;

        _cnt--;
    }

    #endregion
}
