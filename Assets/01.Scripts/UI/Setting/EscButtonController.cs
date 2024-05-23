using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscButtonController : EscUI
{
    private bool isOpendEscPanel = false;
    public bool IsOpendEscPanel => isOpendEscPanel;
    public void ContinueBtnClick()
    {
        HideEscPanel();
    }

    public void HideEscPanel()
    {
        if (!_canHide) return;
        UIManager.Instance.HidePanel("OptionUI");
        UIManager.Instance.HidePanel("EscUI");
        isOpendEscPanel = false;
    }

    public void ShowEscPanel()
    {
        if (!isOpendEscPanel)
        {
            isOpendEscPanel = true;
            UIManager.Instance.ShowPanel("EscUI");
        }
    }

    public void GotoMainMenuBtnClick()
    {
        Application.Quit();
        //LoadingSceneController.LoadScene("StartScene");
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