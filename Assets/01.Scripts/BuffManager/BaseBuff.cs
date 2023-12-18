using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : Buff
{
    [SerializeField] private float intensity; //얼만큼
    [SerializeField] private float randomIntensity; //랜덤크기 (노이즈 개념)
    [SerializeField] private float tick; //얼마시간동안
    [SerializeField] private float randomTick; //랜덤 시간변동

    protected BaseBuilding _onwer;
    public override void SetOnwer(BaseBuilding onwer)
    {
        _onwer = onwer;
    }


}
