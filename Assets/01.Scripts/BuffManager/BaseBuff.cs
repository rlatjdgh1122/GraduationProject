using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : Buff
{
    [SerializeField] private float intensity; //��ŭ
    [SerializeField] private float randomIntensity; //����ũ�� (������ ����)
    [SerializeField] private float tick; //�󸶽ð�����
    [SerializeField] private float randomTick; //���� �ð�����

    protected BaseBuilding _onwer;
    public override void SetOnwer(BaseBuilding onwer)
    {
        _onwer = onwer;
    }


}
