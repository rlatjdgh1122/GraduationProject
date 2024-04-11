using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType
{
    Basic,
    Golden,
}

[Serializable]
public class BoxEvent
{
    [field:SerializeField] public BoxType Type { get; set; }

    #region GoldenBox
    [HideInInspector] public Transform Lid;
    [HideInInspector] public float OpenLidAngle;
    [HideInInspector] public float Duration;
    #endregion
}

public class CostBox : PoolableMono
{
    [SerializeField] private int _cost;
    [SerializeField] private ParticleSystem _clickParticle;
    [SerializeField] private Transform _box;
    [SerializeField] private SoundName _costSound = SoundName.CostBox;

    public BoxEvent InspectorCustomBox;
    private bool _isClick;

    private IEnumerator PlaySequentialTweens()
    {
        yield return InspectorCustomBox.Lid.DOLocalRotate(new Vector3(InspectorCustomBox.OpenLidAngle, 0, 0), InspectorCustomBox.Duration).WaitForCompletion();
        yield return new WaitForSeconds(0.5f); // 임시 값
        ClickEvent();
    }

    private void OnMouseDown()
    {
        if (!_isClick && !WaveManager.Instance.IsBattlePhase)
        {
            switch (InspectorCustomBox.Type)
            {
                case BoxType.Golden:
                    {
                        UIManager.Instance.InitializHudTextSequence();
                        StartCoroutine(PlaySequentialTweens());
                    }
                    break;

                default:
                    {
                        ClickEvent();
                    }
                    break;
            }
        }
        _isClick = true;
    }


    private void ClickEvent()
    {
        if (TutorialManager.Instance.CurTutoQuestIdx == 1) // 일단 퀘스트
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }

        SoundManager.Play2DSound(_costSound);

        _box.gameObject.SetActive(false);

        _clickParticle.Play();

        CostManager.Instance.AddFromCurrentCost(_cost, true, false, transform.position);
        
        StartCoroutine(DisableBox());
    }

    private IEnumerator DisableBox()
    {
        yield return new WaitForSeconds(0.62f);


        PoolManager.Instance.Push(this);
    }
}
