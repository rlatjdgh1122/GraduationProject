using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusPopupUI : PopupUI
{
    protected NexusUIPresenter presenter;

    public override void Awake()
    {
        base.Awake();

        presenter = UIManager.Instance.canvasTrm.Find("NexusUI").GetComponent<NexusUIPresenter>();
    }
}
