using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class StrengthBuffBuilding : BuffBuilding
{
    private Collider[] _colls;
    private Collider[] previousColls = default;

    private Dictionary<int , Penguin> _inRangePenguins = new Dictionary<int , Penguin>();

    private FeedbackPlayer _feedbackPlayer;
    private BuffEffectFeedback _feedbackEffect;

    public override void Init()
    {
        _inRangePenguins.Clear();
    }

    protected override void Awake()
    {
        base.Awake();

        _feedbackPlayer = transform.Find("BuffFeedback").GetComponent<FeedbackPlayer>();
        _feedbackEffect = transform.Find("BuffFeedback").GetComponent<BuffEffectFeedback>();
        
    }

    protected override void Running()
    {
        Collider[] newColls = Physics.OverlapSphere(transform.position, innerDistance, _targetLayer);

        if (previousColls == default || !IsSameColliders(previousColls, newColls))
        {
            previousColls = _colls;
            _colls = newColls;

            if (previousColls != null)
            {
                previousColls = BuffRunning(_feedbackPlayer, _colls, previousColls);
            }
        }
    }

    protected override void BuffEvent()
    {
        int curPreviousCollsLength = previousColls.Length;
        for (int i = 0; i < _colls.Length; i++)
        {
            GameObject obj = _colls[i].gameObject;
            int instanceID = obj.GetInstanceID();

            if (_inRangePenguins.ContainsKey(instanceID))
            {
                continue;
            }

            if (!_inRangePenguins.TryGetValue(instanceID, out Penguin penguin))
            {
                _inRangePenguins.Add(instanceID, _colls[i].GetComponent<Penguin>());
            }

            //임시수정
            _inRangePenguins[instanceID].AddStat(GetBuffValue(), StatType.Damage, StatMode.Increase); 
        }
    }

    protected override void CommenceBuffDecay()
    {
        foreach (var key in _inRangePenguins.Keys)
        {
            bool found = false;
            for (int i = 0; i < _colls.Length; i++)
            {
                GameObject _collObj = _colls[i].gameObject;
                int _collObjInstanceID = _collObj.GetInstanceID();
                if (_collObjInstanceID == key)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                //임시수정
                StartCoroutine(_inRangePenguins[key].RemoveStatCorou(OutoffRangeBuffDuration, GetBuffValue(), StatType.Damage, StatMode.Increase));

                EffectPlayer buffEffect = PoolManager.Instance.Pop(_feedbackEffect.Effect.name) as EffectPlayer;
                buffEffect.transform.SetParent(_inRangePenguins[key].gameObject.transform);
                buffEffect.transform.localPosition = Vector3.zero;
                buffEffect.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

                var main = buffEffect.Particles[0].main;
                main.startSize = 0.3f;

                buffEffect.StartPlay(OutoffRangeBuffDuration);

                _inRangePenguins.Remove(key);
                break;
            }
        }
    }


    protected override void SetBuffValue(int value)
    {
        this.buffValue = value;
    }

    protected override int GetBuffValue()
    {
        return this.buffValue;
    }

    protected override void SetOutoffRangeBuffDuration(float value)
    {
        this.OutoffRangeBuffDuration = value;
    }

    protected override float GetOutoffRangeBuffDuration()
    {
        return this.OutoffRangeBuffDuration;
    }
}
