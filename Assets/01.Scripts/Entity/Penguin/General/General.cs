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
    #region �нú�
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

    #region �нú� �Լ�

    /// <summary>
    /// �� �븶�� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>

    public bool CheckAttackEventPassive(int curAttackCount)
=> passiveData.CheckAttackEventPassive(curAttackCount);

    /// <summary>
    /// �� �ʸ��� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckSecondEventPassive(float curTime)
        => passiveData.CheckSecondEventPassive(curTime);

    /// <summary>
    /// ��ġ�� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
    public bool CheckBackAttackEventPassive()
        => passiveData.CheckBackAttackEventPassive();

    /// <summary>
    /// �ֺ��� �� �� ��� �нú� Ȱ��ȭ Ȯ�� ����
    /// </summary>
    /// <returns> ���</returns>
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
