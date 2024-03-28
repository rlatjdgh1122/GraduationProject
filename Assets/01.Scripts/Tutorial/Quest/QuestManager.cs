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

    private Dictionary<string, QuestData> _allQuests = new Dictionary<string, QuestData>();

    private List<QuestData> _curInprogressQuests = new List<QuestData>();
    public List<QuestData> CurInprogressQuests => _curInprogressQuests; //지금 진행중인 퀘스트

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
        _allQuests.Clear();

        for (int i = 0; i < _questDataSO.QuestDatas.Count; i++) //So에 있는 걸 딕셔너리에 추가함.
        {
            _allQuests.Add(_questDataSO.QuestDatas[i].Id,
                           new QuestData(_questDataSO.QuestDatas[i])); //So니까 실행 끝나고 데이터가 저장되면 안 돼서 생성자를 통해 걍 새로 생성
        }
    }

    public void SetCanStartQuest(string questId)
    {
        QuestData questData = _allQuests[questId];

        questData.QuestStateEnum = QuestState.CanStart;

        _questUI.CreateScrollViewUI(questData); //일단은 여기에 다가 둠. 퀘스트 UI에 퀘스트 추가하는 코드임

        if (questData.IsTutorialQuest)
        {
            StartTutorial(questId);
        }
    }

    public void StartTutorial(string questId)
    {
        QuestData questData = _allQuests[questId];

        if (TutorialManager.Instance.CurQuestIdx != questData.TutorialQuestIdx) //튜토리얼 퀘스트를 순서대로 안 했을때
        {
            Debug.Log($"하... 너는 지금 {TutorialManager.Instance.CurQuestIdx}번째 튜토리얼 퀘스트를 해야하는데" +
                $"{questData.TutorialQuestIdx}번째 퀘스트인 {questData.Id}를 하려고 하잖아;; 리턴함");
            return;
        }

        _dialogSystem.Begin(questData.TutorialTexts); //튜토리얼 텍스트 뜨게
    }

    public void StartQuest(string questId) //퀘스트 시작
    {
        QuestData questData = _allQuests[questId];

        switch (questData.QuestStateEnum)
        {
            case QuestState.Running:
                Debug.Log($"{questData.Id}는 이미 진행중인 퀘스트임 리턴함;;");
                return;
            case QuestState.Finish:
                Debug.Log($"{questData.Id}는 이미 종료한 퀘스트임 리턴함;;");
                return;
            case QuestState.Locked:
                Debug.Log($"{questData.Id}는 아직 못하는 퀘스트임 리턴함;;");
                return;
        }

        Debug.Log($"{questData.Id} 퀘스트 시이작");

        _curInprogressQuests.Add(questData); //현재 진행중 퀘스트 리스트에 추가
        InstantiateQuest(questData);

        _curInprogressQuests[questData.TutorialQuestIdx].QuestStateEnum = QuestState.Running;

        _questUI.UpdatePopUpQuestUI(questData); // 퀘스트 상태 업데이트
        SignalHub.OnStartQuestEvent?.Invoke(); //퀘스트 시작 이벤트

        SignalHub.OnProgressQuestEvent += () => _questUI.UpdateQuestUIToProgress(_allQuests[questData.Id]);
    }

    private void InstantiateQuest(QuestData questData) //퀘스트를 오브젝트로 생성하는 식임. ex: 보석 3개 먹는 퀘스트면 그 아이디의 퀘스트 오브젝트 3개를 생성
    {
        for (int i = 0; i < questData.RepeatCount; i++)
        {
            GameObject questObj = new GameObject(questData.Id);
            questObj.transform.SetParent(transform);
        }
    }

    public void ProgressQuest(string questId) //퀘스트가 진행되었을때. ex: 보석을 먹었을때.
    {
        QuestData questData = _allQuests[questId];

        _allQuests[questData.Id].CurProgressCount++;

        switch (questData.QuestStateEnum)
        {
            case QuestState.Finish:
                Debug.Log($"{questData.Id}는 이미 종료한 퀘스트임 리턴함;;");
                return;
            default:
                if (questData.QuestStateEnum != QuestState.Running)
                {
                    Debug.Log("이거 아직 시작 안 했는데용");
                    return;
                }
                break;
        }

        Transform[] foundChildren = transform.GetComponentsInChildren<Transform>(true);
        Transform[] questObj = Array.FindAll(foundChildren, t => t.name == questData.Id && t != transform);

        if (questObj.Length > 1)
        {
            Destroy(questObj[0].gameObject);
            Debug.Log($"오우 이제 {questData.Id}퀘스트 {questObj.Length}번만 더 해");
            SignalHub.OnProgressQuestEvent?.Invoke(); //퀘스트 진행 이벤트
        }
        else
        {
            EndQuest(questData); //지울 수 없는 오브젝트가 없으면 퀘스트 완료
        }
    }

    public void EndQuest(QuestData questData)
    {
        SignalHub.OnProgressQuestEvent -= () => _questUI.UpdateQuestUIToProgress(_allQuests[questData.Id]);

        Debug.Log($"{questData.Id} 퀘스트 끄읕");

        _curInprogressQuests[questData.TutorialQuestIdx].QuestStateEnum = QuestState.Finish; // 퀘스트에 완료처리 해주고

        _curInprogressQuests.Remove(questData); //현재 진행중 퀘스트 리스트에서 삭제

        if (questData.IsTutorialQuest) { TutorialManager.Instance.IncreaseQuestIdx(); } // 튜토리얼 퀘스트는 순서대로 해야 하니까 idx 증가

        _questUI.RemoveQuestContentUI(questData.Id); // 퀘스트 UI에서 삭제

        SignalHub.OnEndQuestEvent?.Invoke(); //퀘스트 성공 이벤트

        _costUI.CostTween(questData.QuestRewardInfo.RewardCount, true, _questUI.QuestInfoUICompo.RewardPos.position);
        _questUI.QuestInfoUICompo.OffCanvasGroups();
    }

}
