using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscButtonController : EscUI
{
    public void ContinueBtnClick()
    {
        if (!_canHide) return;
        UIManager.Instance.HidePanel("OptionUI");
        UIManager.Instance.HidePanel("EscUI");
    }

    public void GotoMainMenuBtnClick()
    {
        LoadingSceneController.LoadScene("StartScene");
    }

    public void ShowOptionBtnClick()
    {
        UIManager.Instance.ShowPanel("OptionUI");
    }

    public void ExitGameBtnClick()
    {
        Application.Quit();
    } 
}