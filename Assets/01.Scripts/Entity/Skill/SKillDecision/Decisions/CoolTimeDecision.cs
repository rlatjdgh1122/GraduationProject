using UnityEngine;

public class CoolTimeDecsion : SKillDecision
{
    public float MaxCoolTime = 10f;
    private float _lastCoolTime = 0f;

    private void Start()
    {
        _lastCoolTime = -MaxCoolTime; //ó���� ��밡���ϰ�
    }

    public override void Init()
    {
        _lastCoolTime = Time.time;
    }

    public override bool MakeDecision()
    {
        return _lastCoolTime + MaxCoolTime <= Time.time;
    }
}