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

    [SerializeField]
    private float defaultOutoffRangeBuffDuration;
    public float DefaultOutoffRangeBuffDuration => defaultOutoffRangeBuffDuration;

    private float outoffRangeBuffDuration;
    public float OutoffRangeBuffDuration
    {
        get
        {
            return outoffRangeBuffDuration;
        }
        protected set
        {
            outoffRangeBuffDuration = value;
        }
    }


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
        SetBuffValue(DefaultBuffValue);
        SetOutoffRangeBuffDuration(DefaultOutoffRangeBuffDuration);
    }

    public Collider[] BuffRunning(FeedbackPlayer feedbackPlayer, Collider[] _curcolls, Collider[] previousColls)
    {
        if (_curcolls.Length > previousColls.Length)
        {
            feedbackPlayer.PlayFeedback();
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
                feedbackPlayer.FinishFeedback();
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

    protected bool IsSameColliders(Collider[] colliders1, Collider[] colliders2)
    {
        if (colliders1 == null || colliders2 == null || colliders1.Length != colliders2.Length)
        {
            return false;
        }

        HashSet<int> hashSet = new HashSet<int>(); //HashSet�� ����� �ߺ����� Ȯ��
        foreach (var collider in colliders1)
        {
            hashSet.Add(collider.GetInstanceID());
        }

        foreach (var collider in colliders2)
        {
            if (!hashSet.Contains(collider.GetInstanceID()))
            {
                return false;
            }
        }

        return true;
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
    protected abstract void SetOutoffRangeBuffDuration(float value);
    protected abstract float GetOutoffRangeBuffDuration();
}
