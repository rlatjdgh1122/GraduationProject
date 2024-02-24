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

    private bool isChecked = false; //enter, stay, exit�� ���� ����(�ǵ� �ʿ� ����)

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
        // ���� �ȿ� ������ ���� �����̺�Ʈ ����
        BuffEvent();

        Debug.Log("�ȿ����Դ�");
    }

    protected virtual void OnPenguinInsideRangeStay()
    {

    }

    protected virtual void OnPenguinInsideRangeExit()
    {
        CommenceBuffDecay();

        Debug.Log("������");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerDistance);
    }

    protected abstract void BuffEvent(); //���� �ȿ� ������ ����� ���� �̺�Ʈ
    protected abstract void CommenceBuffDecay(); //���� ������ ������ ����� ���� �Ҹ� �̺�Ʈ
    protected abstract void SetBuffValue(int value);
    protected abstract int GetBuffValue();
}
