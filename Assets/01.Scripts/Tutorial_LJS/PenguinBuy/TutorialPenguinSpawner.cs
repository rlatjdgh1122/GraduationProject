using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPenguinSpawner : MonoBehaviour
{
    private Outline _spawnerOutline;
    private bool _onOutline = false;

    private void Awake()
    {
        _spawnerOutline = GetComponent<Outline>();
    }

    private void OnMouseDown()
    {
        ChangeOutline();

        _spawnerOutline.enabled = _onOutline;

        if( _onOutline )
        {
            UIManager.Instance.ShowPanel("StorePanel", true);
        }
        else
        {
            UIManager.Instance.HidePanel("StorePanel");
        }
    }

    private void ChangeOutline()
    {
        _onOutline = !_onOutline;
    }

}