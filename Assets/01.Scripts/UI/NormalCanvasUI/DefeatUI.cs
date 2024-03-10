using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatUI : PopupUI
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    //public void SetTexts()
    //{
    //    _text.text = $"경과 시간 : {_elaspedTime.ToString("F0")}초"; //일단 임시임
    //}

    public void Restart()
    {
        Application.Quit(); //일단 게임 종료
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
