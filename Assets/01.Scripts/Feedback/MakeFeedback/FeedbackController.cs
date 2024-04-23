using System;
using System.Collections.Generic;
using UnityEngine;
public class FeedbackController : MonoBehaviour
{
    private readonly string Cashing_EffectFeedback = "EffectFeedback";
    private Dictionary<EffectFeedbackEnum, EffectFeedback> _effectEnumToFeedbackDic = new();
    private Dictionary<CombatFeedbackEnum, CombatFeedback> _combatEnumToFeedbackDic = new();

    public Feedback CurrentFeedback { get; private set; } = null;
    private void Awake()
    {
        EffectFeedback[] effectFeedbacks = GetComponentsInChildren<EffectFeedback>();

        //�̷��� �ص� �������� �°���..
        foreach (var effectFeedback in effectFeedbacks)
        {
            string typeName = effectFeedback.GetType().Name;
            //��ũ��Ʈ �̸����� EffectFeedback�κ� �����
            string name = typeName.Substring(0, typeName.Length - Cashing_EffectFeedback.Length);

            //Enum�� �̸����� �������� (�̰� ���ٴϱ� ����)
            EffectFeedbackEnum effectEnum = (EffectFeedbackEnum)Enum.Parse(typeof(EffectFeedbackEnum), name);

            _effectEnumToFeedbackDic.Add(effectEnum, effectFeedback);
        }

    }
    public void SpawnFeedback<T>(EffectFeedbackEnum effectEnum) where T : EffectFeedback
    {
        if (!_effectEnumToFeedbackDic.ContainsKey(effectEnum))
        {
            GameObject obj = new GameObject(typeof(T).Name);
            obj.AddComponent<FeedbackPlayer>();
            var feedback = obj.AddComponent<T>();

            obj.transform.parent = transform;

            //add Feedback
            _effectEnumToFeedbackDic.Add(effectEnum, feedback);
        }
        else
        {
            Debug.Log("�̹� �����D�ϴ�");
        }
    }
    public void SpawnFeedback<T>(CombatFeedbackEnum combatEnum) where T : CombatFeedback
    {
        if (!_combatEnumToFeedbackDic.ContainsKey(combatEnum))
        {
            GameObject obj = new GameObject(typeof(T).Name);
            obj.AddComponent<FeedbackPlayer>();
            var feedback = obj.AddComponent<T>();

            obj.transform.parent = transform;

            //add Feedback
            _combatEnumToFeedbackDic.Add(combatEnum, feedback);
        }
        else
        {
            Debug.Log("�̹� �����D�ϴ�");
        }
    }


    public bool TryGetFeedback(EffectFeedbackEnum effectEnum, out EffectFeedback feedback)
    {
        if (_effectEnumToFeedbackDic.TryGetValue(effectEnum, out feedback))
        {
            CurrentFeedback = feedback;
            return true;
        }

        feedback = null;
        return false;
    }

    public bool TryGetFeedback(CombatFeedbackEnum combatEnum, out CombatFeedback feedback, float value)
    {
        if (_combatEnumToFeedbackDic.TryGetValue(combatEnum, out feedback))
        {
            //�˹��̳� ���� ��ġ�� ����
            feedback.Value = value;

            CurrentFeedback = feedback;
            return true;
        }

        feedback = null;
        return false;
    }
}
