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

    private TextMeshProUGUI _curProgressText; // � ����Ʈ����

    private TextMeshProUGUI _questRewardCountText; // ���� ����
    public RectTransform RewardPos => _questRewardCountText.rectTransform; // ���� ����

    private Image _questRewardTypeImg; //� ��������

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
        string questContent = quest.QuestDataCompo.QuestGoalInfo[0].QuestUIDataInfo.QuestContentsInfo; // �ϴ� 0
        Sprite questRewardTypeImg = quest.QuestDataCompo.QuestRewardInfo.RewardTypeImg;
        int questRewardCount = quest.QuestDataCompo.QuestRewardInfo.RewardCount;
        Sprite questTypeIMG = quest.QuestDataCompo.QuestGoalInfo[0].QuestUIDataInfo._questTypeIMG; // �ϴ� 0

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

            QuestInfoBoxUI questInfoBoxUI = Instantiate(_questInfoBoxUIPrefab).GetComponent<QuestInfoBoxUI>(); // �ӽ�
            questInfoBoxUI.transform.SetParent(_questInfoBoxRectParent);

            questInfoBoxUI.SetUpQuestInfoBoxUI(0.5f, questContent, questTypeIMG); // 0.5�� ������ �ӽ� ��

            _questBoxUI.Add(questInfoBoxUI.gameObject);

        }

        //UpdateProgressText($"{quest.QuestGoalList[0].CurrentAmount} / {quest.QuestGoalList[0].RequiredAmount}");
        // ��ǥ 1���ϱ� �ӽ�

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
        _curProgressText.SetText($"���� ���� ��Ȳ: {content}");
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
