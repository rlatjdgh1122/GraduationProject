using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FeedbackController : MonoBehaviour
{
    private readonly string Cashing_EffectFeedback = "EffectFeedback";
    private Dictionary<EffectFeedbackEnum, EffectFeedback> _effectEnumToFeedbackDic = new();
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
    private void Start()
    {
        Debug.Log(_effectEnumToFeedbackDic.Count);
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
            Debug.Log(_effectEnumToFeedbackDic.Count);
            Debug.Log("�̹� �����D�ϴ�");
        }
    }


    public bool TryGetFeedback(EffectFeedbackEnum effectEnum, out EffectFeedback feedback)
    {
        if (_effectEnumToFeedbackDic.TryGetValue(effectEnum, out feedback))
            return true;

        feedback = null;
        return false;
    }
}
