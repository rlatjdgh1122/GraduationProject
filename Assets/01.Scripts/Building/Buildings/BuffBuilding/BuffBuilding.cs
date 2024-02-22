using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBuilding : BaseBuilding
{
    [Header("세탯들")]
    [SerializeField] private float _rangeSize; //범위 크기

    [Header("그 외")]
    [SerializeField] private LayerMask _targetLayer;

    private Collider[] _colls;

    private bool isChecked = false; //enter, stay, exit를 위한 변수(건들 필요 없음)

    [SerializeField]
    [Range(0.0f, 10.0f)]
    protected float buffValue;
    public float BuffValues
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

    private event Action<float> OnInsideRangeEnterEvent = null;  //범위 안에 들어왔을 때 이벤트. 매개변수는 증가수치
    private event Action<float> OnInsideRangeExitEvent = null; 

    protected override void Running()
    {
        _colls = Physics.OverlapSphere(transform.position, _rangeSize, _targetLayer);
        if (_colls.Length > 0)
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
        else
        {
            if (isChecked == true)
            {
                OnPenguinInsideRangeExit();
            }
            isChecked = false;
        }
    }

    protected virtual void OnPenguinInsideRangeEnter()
    {
        // 범위 안에 들어오면 각각 버프이벤트 구독
        OnInsideRangeEnterEvent += BuffEvent;

        OnInsideRangeEnterEvent?.Invoke(buffValue);

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
        Gizmos.DrawWireSphere(transform.position, _rangeSize);
    }

    protected abstract void BuffEvent(float value);
    protected abstract float SetBuffValue(float value);
    protected abstract float GetBuffValue();
}
