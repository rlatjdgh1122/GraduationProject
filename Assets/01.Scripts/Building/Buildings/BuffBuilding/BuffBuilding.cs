using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBuilding : BaseBuilding, IBuffBuilding
{
    [SerializeField]
    [Range(0.0f, 10.0f)]
    protected float innerDistance;

    [SerializeField]
    protected LayerMask _targetLayer;

    [SerializeField]
    private int defaultBuffValue;
    public int DefaultBuffValue => defaultBuffValue;

    private bool isChecked = false; //enter, stay, exit를 위한 변수(건들 필요 없음)

    protected int buffValue;
    public int BuffValues
    {
        get
        {
            return buffValue;
        }
        protected set
        {
            buffValue = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public Collider[] BuffRunning(Collider[] _curcolls, Collider[] previousColls)
    {
        if (_curcolls.Length > previousColls.Length)
        {
            OnPenguinInsideRangeEnter();
            isChecked = true;
        }
        else if(_curcolls.Length < previousColls.Length)
        {
            if (isChecked == true)
            {
                OnPenguinInsideRangeExit();
            }

            if (_curcolls.Length == 0)
            {
                isChecked = false;
            }
        }

        return _curcolls;
    }

    protected virtual void OnPenguinInsideRangeEnter()
    {
        // 범위 안에 들어오면 각각 버프이벤트 구독
        BuffEvent();

        Debug.Log("안에들어왔다");
    }

    protected virtual void OnPenguinInsideRangeStay()
    {

    }

    protected virtual void OnPenguinInsideRangeExit()
    {
        CommenceBuffDecay();

        Debug.Log("나갔다");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerDistance);
    }

    protected abstract void BuffEvent(); //범위 안에 들어오면 실행될 버프 이벤트
    protected abstract void CommenceBuffDecay(); //범위 밖으로 나가면 실행될 버프 소멸 이벤트
    protected abstract void SetBuffValue(int value);
    protected abstract int GetBuffValue();
}
