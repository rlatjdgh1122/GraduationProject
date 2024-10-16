using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _questInfoBoxUIPrefab; 

    [SerializeField]
    private RectTransform _questInfoBoxRectParent;  

    private TextMeshProUGUI _questStateText; // 진행중인지 뜰 텍스트
    private TextMeshProUGUI _questNameText; // 퀘스트 이름

    private TextMeshProUGUI _questRewardCountText; // 보상 갯수
    public RectTransform RewardPos => _questRewardCountText.rectTransform; // 보상 갯수

    private Image _questRewardTypeImg; //어떤 보상인지

    private CanvasGroup _contentsCanvasGroup;
    private CanvasGroup _startButtonCanvasGroup;

    private Button _questStartButton;
    private TextMeshProUGUI _questStartButtonText;

    private Dictionary<Quest, List<QuestInfoBoxUI>> _questBoxUIs = new();

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
        Sprite questRewardTypeImg = quest.QuestDataCompo.QuestRewardInfo.RewardTypeImg;
        int questRewardCount = quest.QuestDataCompo.QuestRewardInfo.RewardCount;

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
        foreach (var questBoxUI in _questBoxUIs.Values) // 일단 다 끄고
        {
            for (int i = 0; i < questBoxUI.Count; i++)
            {
                questBoxUI[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < quest.QuestDataCompo.QuestGoalInfo.Length; i++)
        {
            //QuestInfoBoxUI questInfoBoxUI = PoolManager.Instance.Pop(_questInfoBoxUIPrefab.name)
            //    .GetComponent<QuestInfoBoxUI>();

            QuestInfoBoxUI questInfoBoxUI = Instantiate(_questInfoBoxUIPrefab).GetComponent<QuestInfoBoxUI>(); // 임시
            questInfoBoxUI.transform.SetParent(_questInfoBoxRectParent);

            questInfoBoxUI.SetUpQuestInfoBoxUI(quest.QuestGoalList[i].CurrentAmount,
                                               quest.QuestGoalList[i].RequiredAmount,
                                               quest.QuestDataCompo.QuestGoalInfo[i].QuestUIDataInfo.QuestContentsInfo,
                                               quest.QuestDataCompo.QuestGoalInfo[i].QuestUIDataInfo.QuestTypeIMG);

            if (!_questBoxUIs.ContainsKey(quest))
            {
                _questBoxUIs.Add(quest, new List<QuestInfoBoxUI>());
            }

            _questBoxUIs[quest].Add(questInfoBoxUI);

            questInfoBoxUI.gameObject.SetActive(true); // 여기서 켜줌
        }

        _questStartButtonText.SetText(buttonText);
    }

    public void UpdateQuestBoxUIInfo(Quest quest)
    {
        int idx = 0;
        foreach (var item in _questBoxUIs[quest])
        {
            try
            {
                item.UpdateSliderValue(quest.QuestGoalList[idx].CurrentAmount);
                idx++;
            }
            catch
            {

            }
        }
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
