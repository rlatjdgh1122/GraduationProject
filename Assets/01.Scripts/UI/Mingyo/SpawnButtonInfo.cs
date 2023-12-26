using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnButtonInfo : MonoBehaviour
{
    private Button btn;

    [SerializeField] private Image coolingimg;
    [SerializeField] private float cooltime;
    [SerializeField] PenguinTypeEnum penguinType;

    public SpawnPenguinBtnInfo Info;

    private void Awake()
    {
        btn = GetComponent<Button>();

        Info = new SpawnPenguinBtnInfo(penguinType, btn, coolingimg, cooltime);
    }

    private void Update()
    {
        
    }
}
