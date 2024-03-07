using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPopupUI : PopupUI
{
    protected GeneralPresenter presenter;

    public override void Awake()
    {
        base.Awake();

        presenter = UIManager.Instance.canvasTrm.Find("GeneralStore").GetComponent<GeneralPresenter>();
    }
}
