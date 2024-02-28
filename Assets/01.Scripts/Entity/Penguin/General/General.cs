using Bitgem.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LigeonStat
{
    public StatType type;
    public int value = 10;
}


[Serializable]
public class LigeonStatAdjustment
{
    public List<LigeonStat> IncStat;
    public List<LigeonStat> DecStat;
}
public class General : Penguin
{
    #region 패시브
    public PassiveDataSO passiveData = null;
    #endregion
    public LigeonStatAdjustment ligeonStat = null;


    protected override void Awake()
    {
        base.Awake();

        passiveData?.SetOwner(this);
    }

    protected override void Start()
    {
        base.Start();

        if (passiveData == true)
        {
            passiveData.Start();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (passiveData == true)
            passiveData.Update();

    }

    #region 패시브 함수

    /// <summary>
    /// 몇 대마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>

    public bool CheckAttackEventPassive(int curAttackCount)
=> passiveData.CheckAttackEventPassive(curAttackCount);

    /// <summary>
    /// 몇 초마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckSecondEventPassive(float curTime)
        => passiveData.CheckSecondEventPassive(curTime);

    /// <summary>
    /// 뒤치기 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckBackAttackEventPassive()
        => passiveData.CheckBackAttackEventPassive();

    /// <summary>
    /// 주변의 적 수 비례 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckAroundEnemyCountEventPassive()
        => passiveData.CheckAroundEnemyCountEventPassive();
#endregion

    public virtual void OnPassiveAttackEvent()
    {

    }
    public virtual void OnPassiveSecondEvent()
    {

    }
    public virtual void OnPassiveBackAttackEvent()
    {

    }
    public virtual void OnPassiveAroundEvent()
    {

    }
}
