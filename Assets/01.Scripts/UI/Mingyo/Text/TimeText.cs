using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeText : BaseUIText
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void SetUp()
    {
        base.SetUp();
    }

    private void OnEnable()
    {
        SetUp();
    }
}
