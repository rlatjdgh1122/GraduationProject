using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBuilding : BaseBuilding
{
    [Header("���ȵ�")]
    [SerializeField] private float _rangeSize; //���� ũ��

    [Header("�� ��")]
    [SerializeField] private LayerMask _targetLayer;

    private Collider[] _colls;

    private bool isChecked = false; //enter, stay, exit�� ���� ����(�ǵ� �ʿ� ����)

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

    private event Action<float> OnInsideRangeEnterEvent = null;  //���� �ȿ� ������ �� �̺�Ʈ. �Ű������� ������ġ
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
        // ���� �ȿ� ������ ���� �����̺�Ʈ ����
        OnInsideRangeEnterEvent += BuffEvent;

        OnInsideRangeEnterEvent?.Invoke(buffValue);

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
        Gizmos.DrawWireSphere(transform.position, _rangeSize);
    }

    protected abstract void BuffEvent(float value);
    protected abstract float SetBuffValue(float value);
    protected abstract float GetBuffValue();
}
