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

    private event Action OnInsideRangeEnterEvent = null;  //범위 안에 들어왔을 때 이벤트
    private event Action<int> OnInsideRangeExitEvent = null;

    protected override void Awake()
    {
        base.Awake();
    }

    public int BuffRunning(Collider[] _curcolls, int previousCollsLength)
    {
        if (_curcolls.Length > previousCollsLength)
        {
            if (isChecked == true)
            {
                OnPenguinInsideRangeStay();
            }
            else
            {
                OnPenguinInsideRangeEnter();
            }
            isChecked = true;
        }
        else if(_curcolls.Length == 0)
        {
            if (isChecked == true)
            {
                OnPenguinInsideRangeExit();
            }
            isChecked = false;
        }

        return _curcolls.Length;
    }

    protected virtual void OnPenguinInsideRangeEnter()
    {
        // 범위 안에 들어오면 각각 버프이벤트 구독
        OnInsideRangeEnterEvent += BuffEvent;

        OnInsideRangeEnterEvent?.Invoke();

        Debug.Log("안에들어왔다");
    }

    protected virtual void OnPenguinInsideRangeStay()
    {

    }

    protected virtual void OnPenguinInsideRangeExit()
    {
        OnInsideRangeEnterEvent -= BuffEvent;

        Debug.Log("나갔다");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerDistance);
    }

    protected abstract void BuffEvent();
    protected abstract void SetBuffValue(int value);
    protected abstract int GetBuffValue();
}
