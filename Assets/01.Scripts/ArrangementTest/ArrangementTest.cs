using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementTest : MonoBehaviour
{
    [SerializeField] private float distance = 3;
    [SerializeField] private int width = 5;
    [SerializeField] private int length = 7;
    private void Awake()
    {
        SignalHub.OnArrangementInfoModify += SettingArrangement;
    }
    private void OnDestroy()
    {
        SignalHub.OnArrangementInfoModify -= SettingArrangement;
    }

    private void Start()
    {
        
    }

    private void SettingArrangement(ArrangementInfo info)
    {

    }
}
