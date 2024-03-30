using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoUI : MonoBehaviour
{
    private TextMeshProUGUI _questStateText; // 진행중인지 뜰 텍스트
    private TextMeshProUGUI _questNameText; // 퀘스트 이름

    private TextMeshProUGUI _questContentText; // 어떤 퀘스트인지
    private TextMeshProUGUI _curProgressText; // 어떤 퀘스트인지

    private TextMeshProUGUI _questRewardCountText; // 보상 갯수
    public RectTransform RewardPos => _questRewardCountText.rectTransform; // 보상 갯수

    private Image _questRewardTypeImg; //어떤 보상인지

    private CanvasGroup _contentsCanvasGroup;
    private CanvasGroup _startButtonCanvasGroup;

    private Button _questStartButton;
    private TextMeshProUGUI _questStartButtonText;

    private void Awake()
    {
        Transform contents = transform.Find("Contents").transform;

        _contentsCanvasGroup = contents.GetComponent<CanvasGroup>();

        Transform popUpBox = contents.Find("BoxText").transform;

        _questStateText = popUpBox.Find("State").GetComponent<TextMeshProUGUI>();

        _questNameText = popUpBox.Find("QuestName").GetComponent<TextMeshProUGUI>();

        _questContentText = popUpBox.Find("Description01").GetComponent<TextMeshProUGUI>();
        _curProgressText = popUpBox.Find("Description02").GetComponent<TextMeshProUGUI>();


        Transform rewardBox = contents.Find("BoxReward").transform;

        _questRewardTypeImg = rewardBox.Find("key").GetComponent<Image>();

        _questRewardCountText = rewardBox.Find("key/rewardCount").GetComponent<TextMeshProUGUI>();

        Transform buttonTrm = transform.root.Find("QuestUI/StartButton").transform;
        _questStartButton = buttonTrm.GetComponent<Button>();
        _startButtonCanvasGroup = buttonTrm.GetComponent<CanvasGroup>();
        _questStartButtonText = buttonTrm.GetChild(0).GetComponent<TextMeshProUGUI>();

        SignalHub.OnOffPopUiEvent += OffCanvasGroups;
    }

    public void UpdatePopUpQuestUI(QuestData questData, Action action = null)
    {
        QuestState questState = questData.QuestStateEnum;
        string questName = questData.Id;
        string questContent = questData.QuestUIDataInfo.QuestContentsInfo;
        Sprite questRewardTypeImg = questData.QuestRewardInfo.RewardTypeImg;
        int questRewardCount = questData.QuestRewardInfo.RewardCount;

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
                _questStartButton.onClick.AddListener(() => StartQuest(questName));
                break;
            case QuestState.Running:
                questStateText = "진행 중";
                buttonText = "퀘스트 완료";

                _questStartButton.interactable = false;
                break;
            case QuestState.CanFinish:
                _questStartButton.interactable = true;
                _questStartButton.onClick.AddListener(() => EndQuest(questName, action));


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
        _questContentText.SetText(questContent);
        _questRewardTypeImg.sprite = questRewardTypeImg;
        _questRewardCountText.SetText(questRewardCount.ToString());
        UpdateProgressText($"{questData.CurProgressCount} / {questData.RepeatCount}");

        _questStartButtonText.SetText(buttonText);
    }

    private void StartQuest(string questId)
    {
        QuestManager.Instance.StartQuest(questId);
    }

    private void EndQuest(string questId, Action action)
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
