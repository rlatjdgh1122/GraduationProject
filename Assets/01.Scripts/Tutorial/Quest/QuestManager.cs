using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField]
    private QuestDataSO _questDataSO;

    private Dictionary<string, QuestData> _allQuestsData = new Dictionary<string, QuestData>();
    private Dictionary<string, Quest> _curInprogressQuests = new();
    private Dictionary<string, Quest> _canStartQuests = new();

    private DialogSystem _dialogSystem;
    private QuestUI _questUI;
    private CostUI _costUI;

    public override void Awake()
    {
        base.Awake();

        _dialogSystem = Object.FindAnyObjectByType<DialogSystem>();
        _questUI = Object.FindAnyObjectByType<QuestUI>();
        _costUI = Object.FindAnyObjectByType<CostUI>();
    }

    private void Start()
    {
        LoadQuestData();
    }

    private void LoadQuestData()
    {
        _allQuestsData.Clear();

        for (int i = 0; i < _questDataSO.QuestDatas.Count; i++) //So에 있는 걸 딕셔너리에 추가함.
        {
            _allQuestsData.Add(_questDataSO.QuestDatas[i].Id,
                               _questDataSO.QuestDatas[i]);
        }
    }

    public void SetCanStartQuest(string questId)
    {
        QuestData questData = GetQuestData(questId);
        // 새로운 퀘스트 생성
        Quest quest = new Quest(questData);
        _canStartQuests.Add(questId, quest);

        quest.SetQuestState(QuestState.CanStart);

        _questUI.CreateScrollViewUI(quest); // 퀘스트 UI에 퀘스트 추가하는 코드

        if (quest.QuestDataCompo.TutorialQuestInfo.IsTutorialQuest)
        {
            StartTutorial(questId);
        }
    }

    public void StartTutorial(string questId)
    {
        if (questId == null)
        {
            return;
        }

        Quest quest = GetCanStartQuests(questId);

        #region DebugByCase
        if (TutorialManager.Instance.CurTutoQuestIdx != quest.QuestDataCompo.TutorialQuestInfo.TutorialQuestIdx) //튜토리얼 퀘스트를 순서대로 안 했을때
        {
            Debug.Log($"하... 너는 지금 {TutorialManager.Instance.CurTutoQuestIdx}번째 튜토리얼 퀘스트를 해야하는데" +
                $"{quest.QuestDataCompo.TutorialQuestInfo.TutorialQuestIdx}번째 퀘스트인 {quest.QuestId}를 하려고 하잖아;; 리턴함");
            return;
        }
        #endregion

        _dialogSystem.Begin(quest.QuestDataCompo.TutorialQuestInfo.TutorialTexts); //튜토리얼 텍스트 뜨게
        StartQuest(quest.QuestId); // 튜토리얼은 버튼 눌러서 시작이 아니라 그냥 시작하게
    }

    public void StartQuest(string questId) //퀘스트 시작
    {
        QuestData questData = GetQuestData(questId);
        Quest quest = GetCanStartQuests(questId);

        #region DebugByCase
        switch (quest.QuestStateEnum)
        {
            case QuestState.Running:
                Debug.Log($"{quest.QuestId}는 이미 진행중인 퀘스트임 리턴함;;");
                return;
            case QuestState.Finish:
                Debug.Log($"{quest.QuestId}는 이미 종료한 퀘스트임 리턴함;;");
                return;
            case QuestState.Locked:
                Debug.Log($"{quest.QuestId}는 아직 못하는 퀘스트임 리턴함;;");
                return;
            case QuestState.CanFinish:
                Debug.Log($"{quest.QuestId}는 이미 진행중인 퀘스트임 리턴함;;");
                break;
        }
        #endregion

        Debug.Log($"{questData.Id} 퀘스트 시이작");

        _canStartQuests.Remove(questId); // 실제로 시작하였으니 시작가능 퀘스트에서 삭제

        for (int i = 0; i < quest.QuestDataCompo.QuestGoalInfo.Length; i++)
        {
            quest.QuestGoalList.Add(new QuestGoal
                (questData.QuestGoalInfo[i].QuestGoalType,
                 questData.QuestGoalInfo[i].RequiredAmount,
                 questData.QuestGoalInfo[i].GoalId)); // 목표 추가
        }

        quest.SetQuestState(QuestState.Running); // 퀘스트 상태 업데이트
        _curInprogressQuests.Add(quest.QuestId, quest); // 활성 퀘스트에 추가

        _questUI.UpdatePopUpQuestUI(quest); // 퀘스트 UI상태 업데이트
        SignalHub.OnStartQuestEvent?.Invoke(); //퀘스트 시작 이벤트

    }

    public void ProgressQuest(string questId, string goalID) //퀘스트가 진행되었을때. ex: 보석을 먹었을때.
    {
        QuestData questData = GetQuestData(questId);
        Quest quest = GetRunningQuest(questId);

        #region DebugByCase
        switch (quest.QuestStateEnum)
        {
            case QuestState.Finish:
                Debug.Log($"{questData.Id}는 이미 종료한 퀘스트임 리턴함;;");
                return;
            default:
                if(quest.QuestStateEnum == QuestState.CanFinish)
                {
                    Debug.Log("오우 이거 완료 가능함");
                    return;
                }
                if (quest.QuestStateEnum != QuestState.Running)
                {
                    Debug.Log($"이거 아직 시작 안 했는데용 너 지금 {questData.QuestStateEnum}임"); 
                    return;
                }
                break;
        }
        #endregion

        quest.UpdateCondition(goalID);
        SignalHub.OnProgressQuestEvent?.Invoke(); // 퀘스트 진행 이벤트

        _questUI.UpdateQuestUIToProgress(quest);

        if (quest.IsCompleted()) // 완료했다면
        {
            _questUI.UpdatePopUpQuestUI(quest);
            _questUI.SetCautionBoxImage(true);

            if (questData.TutorialQuestInfo.IsTutorialQuest) // 튜토리얼이면 바로 완료처리
            {
                EndQuest(questId);
                return;
            }

            quest.SetQuestState(QuestState.CanFinish); // 퀘스트 완료가능하게
        }
        else // 아직 남았다면
        {
            for (int i = 0; i < questData.QuestGoalInfo.Length; i++)
            {
                Debug.Log($"오우 이제 {questData.Id}퀘스트의 {i + 1}번째 퀘스트" +
                $"{quest.QuestGoalList[i].RequiredAmount}번 중에 {quest.QuestGoalList[i].CurrentAmount}번 함");
            }
            
        }
    }

    public void EndQuest(string questId)
    {
        QuestData questData = GetQuestData(questId);
        Quest quest = GetRunningQuest(questId);

        Debug.Log($"{questData.Id} 퀘스트 끄읕");

        quest.SetQuestState(QuestState.Finish); // 퀘스트에 완료처리 해주고
        _questUI.UpdatePopUpQuestUI(quest);

        _curInprogressQuests.Remove(questData.Id); //현재 진행중 퀘스트 리스트에서 삭제

        SignalHub.OnEndQuestEvent?.Invoke(); //퀘스트 성공 이벤트

        _costUI.CostTween(questData.QuestRewardInfo.RewardCount, true, _questUI.QuestInfoUICompo.RewardPos.position);
        _questUI.QuestInfoUICompo.OffCanvasGroups();

        if (questData.TutorialQuestInfo.IsTutorialQuest)
        {
            SetCanStartQuest(TutorialManager.Instance.GetNextTutorialQuest()); // 다음 튜토리얼 시작
        }

        //_questUI.RemoveQuestContentUI(questData.Id); // 완료된 퀘스트 UI에 추가
    }

    public QuestData GetQuestData(string questId) => _allQuestsData[questId];
    public Quest GetRunningQuest(string questId) => _curInprogressQuests[questId];
    public Quest GetCanStartQuests(string questId) => _canStartQuests[questId];

    public bool IsQuestActive(string questId)
    {
        QuestData questData = _allQuestsData[questId];
        if (questData.QuestStateEnum.Equals(QuestState.Running))
        {
            return true;
        }
        return false;
    }
}
