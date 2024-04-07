using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class QuestInfoUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _questInfoBoxUIPrefab; 

    [SerializeField]
    private RectTransform _questInfoBoxRectParent;

    private TextMeshProUGUI _questStateText; // 진행중인지 뜰 텍스트
    private TextMeshProUGUI _questNameText; // 퀘스트 이름

    private TextMeshProUGUI _curProgressText; // 어떤 퀘스트인지

    private TextMeshProUGUI _questRewardCountText; // 보상 갯수
    public RectTransform RewardPos => _questRewardCountText.rectTransform; // 보상 갯수

    private Image _questRewardTypeImg; //어떤 보상인지

    private CanvasGroup _contentsCanvasGroup;
    private CanvasGroup _startButtonCanvasGroup;

    private Button _questStartButton;
    private TextMeshProUGUI _questStartButtonText;

    private List<GameObject> _questBoxUI = new List<GameObject>();

    private void Awake()
    {
        Transform contents = transform.Find("Contents").transform;

        _contentsCanvasGroup = contents.GetComponent<CanvasGroup>();

        Transform popUpBox = contents.Find("BoxText").transform;

        _questStateText = popUpBox.Find("State").GetComponent<TextMeshProUGUI>();

        _questNameText = popUpBox.Find("QuestName").GetComponent<TextMeshProUGUI>();

        Transform rewardBox = contents.Find("BoxReward").transform;

        _questRewardTypeImg = rewardBox.Find("key").GetComponent<Image>();

        _questRewardCountText = rewardBox.Find("key/rewardCount").GetComponent<TextMeshProUGUI>();

        Transform buttonTrm = transform.root.Find("QuestUI/StartButton").transform;
        _questStartButton = buttonTrm.GetComponent<Button>();
        _startButtonCanvasGroup = buttonTrm.GetComponent<CanvasGroup>();
        _questStartButtonText = buttonTrm.GetChild(0).GetComponent<TextMeshProUGUI>();

        SignalHub.OnOffPopUiEvent += OffCanvasGroups;
    }

    public void UpdatePopUpQuestUI(Quest quest ,Action action = null)
    {
        QuestState questState = quest.QuestStateEnum;
        string questName = quest.QuestId;
        string questContent = quest.QuestDataCompo.QuestGoalInfo[0].QuestUIDataInfo.QuestContentsInfo; // 일단 0
        Sprite questRewardTypeImg = quest.QuestDataCompo.QuestRewardInfo.RewardTypeImg;
        int questRewardCount = quest.QuestDataCompo.QuestRewardInfo.RewardCount;
        Sprite questTypeIMG = quest.QuestDataCompo.QuestGoalInfo[0].QuestUIDataInfo._questTypeIMG; // 일단 0

        _questStartButton.onClick.RemoveAllListeners();

        _contentsCanvasGroup.alpha = 1;
        _startButtonCanvasGroup.alpha = 1;

        string questStateText = null;
        string buttonText = null;
        switch (questState)
        {
            case QuestState.Locked:
                questStateText = "시작 불가능";
                break;
            case QuestState.CanStart:
                questStateText = "시작 가능";
                buttonText = "퀘스트 시작하기";
                _questStartButton.onClick.AddListener(() => StartQuestHandler(questName));
                break;
            case QuestState.Running:
                questStateText = "진행 중";
                buttonText = "퀘스트 완료";

                _questStartButton.interactable = false;
                break;
            case QuestState.CanFinish:
                _questStartButton.interactable = true;
                _questStartButton.onClick.AddListener(() => EndQuestHandler(questName, action));


                questStateText = "완료 가능";
                buttonText = "퀘스트 완료";
                break;
            case QuestState.Finish:
                questStateText = "완료";
                buttonText = "퀘스트 완료";
                _questStartButton.interactable = false;
                break;
        }

        _questStateText.SetText(questStateText);

        _questNameText.SetText(questName);
        _questRewardTypeImg.sprite = questRewardTypeImg;
        _questRewardCountText.SetText(questRewardCount.ToString());

        // 임시
        for (int i = 0; i < _questBoxUI.Count; i++)
        {
            Destroy(_questBoxUI[i].gameObject);
        }

        _questBoxUI.Clear();

        Debug.Log(_questBoxUI.Count);

        for (int i = 0; i < quest.QuestDataCompo.QuestGoalCount; i++)
        {
            //QuestInfoBoxUI questInfoBoxUI = PoolManager.Instance.Pop(_questInfoBoxUIPrefab.name)
            //    .GetComponent<QuestInfoBoxUI>();

            QuestInfoBoxUI questInfoBoxUI = Instantiate(_questInfoBoxUIPrefab).GetComponent<QuestInfoBoxUI>(); // 임시
            questInfoBoxUI.transform.SetParent(_questInfoBoxRectParent);

            questInfoBoxUI.SetUpQuestInfoBoxUI(0.5f, questContent, questTypeIMG); // 0.5는 레전드 임시 값

            _questBoxUI.Add(questInfoBoxUI.gameObject);

        }

        //UpdateProgressText($"{quest.QuestGoalList[0].CurrentAmount} / {quest.QuestGoalList[0].RequiredAmount}");
        // 목표 1개니까 임시

        _questStartButtonText.SetText(buttonText);
    }

    private void StartQuestHandler(string questId)
    {
        QuestManager.Instance.StartQuest(questId);
    }

    private void EndQuestHandler(string questId, Action action)
    {
        action?.Invoke();
        QuestManager.Instance.EndQuest(questId);
    }

    public void UpdateProgressText(string content)
    {
        _curProgressText.SetText($"현재 진행 상황: {content}");
    }

    public void QuestStartBtn(string questId)
    {
        QuestManager.Instance.StartQuest(questId);


        SignalHub.OnStartQuestEvent?.Invoke();
    }

    public void OffCanvasGroups()
    {
        _contentsCanvasGroup.alpha = 0;
        _startButtonCanvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        SignalHub.OnOffPopUiEvent -= OffCanvasGroups;
    }
}
