using Bitgem.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class General : Penguin
{
    //public PassiveDataSO passiveData = null;
    public Skill skill;

    public bool canSpinAttack = false;

    protected override void Awake()
    {
        base.Awake();

        skill = transform.Find("SkillManager").GetComponent<Skill>();
        skill?.SetOwner(this);
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

    /*public bool CheckAttackEventPassive(int curAttackCount)
=> passiveData.CheckAttackEventPassive(curAttackCount);*/

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

    public override void OnPassiveAttackEvent()
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
