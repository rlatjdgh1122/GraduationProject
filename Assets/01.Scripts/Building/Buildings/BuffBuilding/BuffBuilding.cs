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

    private event Action OnInsideRangeEnterEvent = null;  //���� �ȿ� ������ �� �̺�Ʈ
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
        // ���� �ȿ� ������ ���� �����̺�Ʈ ����
        OnInsideRangeEnterEvent += BuffEvent;

        OnInsideRangeEnterEvent?.Invoke();

        Debug.Log("�ȿ����Դ�");
    }

    protected virtual void OnPenguinInsideRangeStay()
    {

    }

    protected virtual void OnPenguinInsideRangeExit()
    {
        OnInsideRangeEnterEvent -= BuffEvent;

        Debug.Log("������");
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
