using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPopupUI : PopupUI
{
    protected GeneralUIPresenter presenter;

    public override void Awake()
    {
        base.Awake();

        presenter = UIManager.Instance.canvasTrm.Find("GeneralUI").GetComponent<GeneralUIPresenter>();
    }
}
