using System;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    private readonly string Cashing_SoundFeedbacks = "SoundFeedbacks";
    private readonly string Cashing_HitSoundFeedbacks = "HitSoundFeedbacks";
    private readonly string Cashing_FeedbackName = "Feedback";
    private readonly string Cashing_SoundFeedbackName = "Feedback";
    private readonly string Cashing_HitSoundFeedbackName = "Feedback";

    private readonly Dictionary<FeedbackEnumType, FeedbackPlayer> _effectEnumToFeedbackDic = new();
    private readonly Dictionary<SoundFeedbackEnumType, SoundFeedback> _soundEnumToFeedbackDic = new();
    private readonly Dictionary<HitSoundFeedbackEnumType, SoundFeedback> _soundHitEnumToFeedbackDic = new();

    public FeedbackPlayer CurrentFeedback { get; private set; } = null;

    private void Awake()
    {
        SetFeedbackPlayer();
        SetSoundFeedback();
        SetHitSoundFeedback();
    }

    private void SetFeedbackPlayer()
    {
        FeedbackPlayer[] effectFeedbacks = GetComponentsInChildren<FeedbackPlayer>();

        foreach (var effectFeedback in effectFeedbacks)
        {
            string typeName = effectFeedback.name;
            string name = typeName.Substring(0, typeName.Length - Cashing_FeedbackName.Length);

            if (Enum.TryParse(name, true, out FeedbackEnumType effectEnum))
                _effectEnumToFeedbackDic.Add(effectEnum, effectFeedback);
        }

    }

    private void SetSoundFeedback()
    {
        SoundFeedback[] effectFeedbacks = GetComponentsInChildren<SoundFeedback>();

        foreach (var effectFeedback in effectFeedbacks)
        {
            string typeName = effectFeedback.name;
            string name = typeName.Substring(0, typeName.Length - Cashing_SoundFeedbackName.Length);

            if (Enum.TryParse(name, true, out SoundFeedbackEnumType effectEnum))
                _soundEnumToFeedbackDic.Add(effectEnum, effectFeedback);
        }
    }

    private void SetHitSoundFeedback()
    {
        SoundFeedback[] effectFeedbacks = GetComponentsInChildren<SoundFeedback>();

        foreach (var effectFeedback in effectFeedbacks)
        {
            string typeName = effectFeedback.name;
            string name = typeName.Substring(0, typeName.Length - Cashing_SoundFeedbackName.Length);

            if (Enum.TryParse(name, true, out HitSoundFeedbackEnumType effectEnum))
                _soundHitEnumToFeedbackDic.Add(effectEnum, effectFeedback);
        }
    }

    public bool TryGetFeedback(FeedbackEnumType effectEnum, out FeedbackPlayer feedback, float value = 0)
    {
        if (_effectEnumToFeedbackDic.TryGetValue(effectEnum, out feedback))
        {
            feedback.Value = value;
            CurrentFeedback = feedback;
            return true;
        }

        feedback = null;
        return false;
    }

    public bool TryPlaySoundFeedback(SoundFeedbackEnumType soundEnum)
    {
        if (_soundEnumToFeedbackDic.TryGetValue(soundEnum, out var soundFeedback))
        {
            soundFeedback.StartFeedback();
            return true;
        }
        return false;
    }

    public bool TryPlayHitSoundFeedback(HitSoundFeedbackEnumType soundEnum)
    {
        if (_soundHitEnumToFeedbackDic.TryGetValue(soundEnum, out var soundFeedback))
        {
            soundFeedback.StartFeedback();
            return true;
        }
        return false;
    }

    #region 스폰 함수 (에디터용)
    public void SpawnFeedback<T1>(FeedbackEnumType effectEnum) where T1 : Feedback
    {
        GameObject obj = new GameObject($"{effectEnum.ToString()}{Cashing_FeedbackName}");
        obj.AddComponent<FeedbackPlayer>();
        obj.AddComponent<T1>();

        obj.transform.parent = transform;
    }

    public void SpawnFeedback<T1, T2>(FeedbackEnumType effectEnum) where T1 : Feedback where T2 : Feedback
    {
        GameObject obj = new GameObject($"{effectEnum.ToString()}{Cashing_FeedbackName}");
        obj.AddComponent<FeedbackPlayer>();
        obj.AddComponent<T1>();
        obj.AddComponent<T2>();

        obj.transform.parent = transform;
    }

    public void SpawnSoundFeedback(SoundFeedbackEnumType soundName, SoundName setSound = SoundName.MeleeAttack)
    {
        var trm = transform.Find(Cashing_SoundFeedbacks);
        if (trm == null)
        {
            GameObject newObj = new GameObject(Cashing_SoundFeedbacks);
            newObj.transform.parent = transform;

            trm = newObj.transform;
        }
        GameObject obj = new GameObject($"{soundName.ToString()}{Cashing_SoundFeedbackName}");
        obj.AddComponent<SoundFeedback>().soundName = setSound;

        obj.transform.parent = trm;
    }

    public void SpawnHitSoundFeedback(HitSoundFeedbackEnumType soundName, SoundName setSound = SoundName.MeleeAttack)
    {
        var trm = transform.Find(Cashing_HitSoundFeedbacks);
        if (trm == null)
        {
            GameObject newObj = new GameObject(Cashing_HitSoundFeedbacks);
            newObj.transform.parent = transform;

            trm = newObj.transform;
        }
        GameObject obj = new GameObject($"{soundName.ToString()}{Cashing_HitSoundFeedbackName}");
        obj.AddComponent<SoundFeedback>().soundName = setSound;

        obj.transform.parent = trm;
    }
    #endregion

}
