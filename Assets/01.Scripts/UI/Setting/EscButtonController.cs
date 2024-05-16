using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscButtonController : MonoBehaviour
{
    public void ContinueBtnClick()
    {
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