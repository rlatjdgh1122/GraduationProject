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

    #region 패시브 함수

    /// <summary>
    /// 몇 대마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>

    /*public bool CheckAttackEventPassive(int curAttackCount)
=> passiveData.CheckAttackEventPassive(curAttackCount);*/

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
