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
    //    _text.text = $"��� �ð� : {_elaspedTime.ToString("F0")}��"; //�ϴ� �ӽ���
    //}

    public void Restart()
    {
        Application.Quit(); //�ϴ� ���� ����
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
