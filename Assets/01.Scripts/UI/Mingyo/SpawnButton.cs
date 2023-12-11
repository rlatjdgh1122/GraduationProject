using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnButton : MonoBehaviour
{
    private Button btn;
    [SerializeField]
    private Image coolingimg;
    [SerializeField] private float cooltime;

    public SpawnPenguinBtnInfo Info;

    private void Awake()
    {
        btn = GetComponent<Button>();

        Info = new SpawnPenguinBtnInfo(btn, coolingimg, cooltime);
    }
}
