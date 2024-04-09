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

    private TextMeshProUGUI _questStateText; // ���������� �� �ؽ�Ʈ
    private TextMeshProUGUI _questNameText; // ����Ʈ �̸�

    private TextMeshProUGUI _questRewardCountText; // ���� ����
    public RectTransform RewardPos => _questRewardCountText.rectTransform; // ���� ����

    private Image _questRewardTypeImg; //� ��������

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
                questStateText = "���� �Ұ���";
                break;
            case QuestState.CanStart:
                questStateText = "���� ����";
                buttonText = "����Ʈ �����ϱ�";
                _questStartButton.onClick.AddListener(() => StartQuestHandler(questName));
                break;
            case QuestState.Running:
                questStateText = "���� ��";
                buttonText = "����Ʈ �Ϸ�";

                _questStartButton.interactable = false;
                break;
            case QuestState.CanFinish:
                _questStartButton.interactable = true;
                _questStartButton.onClick.AddListener(() => EndQuestHandler(questName, action));


                questStateText = "�Ϸ� ����";
                buttonText = "����Ʈ �Ϸ�";
                break;
            case QuestState.Finish:
                questStateText = "�Ϸ�";
                buttonText = "����Ʈ �Ϸ�";
                _questStartButton.interactable = false;
                break;
        }

        _questStateText.SetText(questStateText);

        _questNameText.SetText(questName);
        _questRewardTypeImg.sprite = questRewardTypeImg;
        _questRewardCountText.SetText(questRewardCount.ToString());

        // �ӽ�
        foreach (var questBoxUI in _questBoxUIs.Values) // �ϴ� �� ����
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

            QuestInfoBoxUI questInfoBoxUI = Instantiate(_questInfoBoxUIPrefab).GetComponent<QuestInfoBoxUI>(); // �ӽ�
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

            questInfoBoxUI.gameObject.SetActive(true); // ���⼭ ����
        }

        _questStartButtonText.SetText(buttonText);
    }

    public void UpdateQuestBoxUIInfo(Quest quest)
    {
        // �̷��� �ϸ� ������ ������Ʈ �� UI�� ���� QuestInfoUI�� �ƴϸ� ���� ����
        // �Ƹ� QuestInfoBoxUI�� ������ �ƴ϶� ������Ʈ �ϴ� ������� �ؾ� �� ��.

        for (int i = 0; i < _questBoxUIs[quest].Count; i++)
        {
            _questBoxUIs[quest][i].UpdateSliderValue(quest.QuestGoalList[i].CurrentAmount);
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
