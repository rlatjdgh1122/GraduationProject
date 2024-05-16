using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ArmySystem;
using UnityEngine.UI;
public class MoveModeUI : MonoBehaviour
{
    public TextMeshProUGUI MoveModeText = null;
    public Image Icon = null;
    public Image BackGround = null;

    [Header("�����")]
    public Sprite BattleIcon = null;
    public Sprite CommandIcon = null;

    [Header("��ƾ�")]
    public Color BattleColor = Color.white;
    public Color CommandColor = Color.white;

    //A42A20 - ��
    //2D5C34 - ��

    private void Awake()
    {
        SignalHub.OnBattleModeChanged += OnBattleModeChangedHandler;
    }

    private void OnBattleModeChangedHandler(MovefocusMode mode)
    {
        if (mode == MovefocusMode.Battle)
        {
            MoveModeText.text = "���� ���";
            Icon.sprite = BattleIcon;
            BackGround.color = BattleColor;
        }
        else
        {
            MoveModeText.text = "��� ���";
            Icon.sprite = CommandIcon;
            BackGround.color = CommandColor;
        }
    }

    private void OnDisable()
    {
        SignalHub.OnBattleModeChanged -= OnBattleModeChangedHandler;
    }
}
