using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatUI : PopupUI
{
    [SerializeField] private AudioVolumeController _audioController;

    public override void Awake()
    {
        base.Awake();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        
        SoundManager.StopBGM(SoundName.NormalBattleBGM);
        _audioController.SFXVolume(0);
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
        UIManager.Instance.ShowPanel("CreditUI");
        //Application.Quit(); //�ϴ� ���� ����
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
