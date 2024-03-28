using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoUI : MonoBehaviour
{
    private TextMeshProUGUI _questStateText; // ���������� �� �ؽ�Ʈ
    private TextMeshProUGUI _questNameText; // ����Ʈ �̸�

    private TextMeshProUGUI _questContentText; // � ����Ʈ����
    private TextMeshProUGUI _curProgressText; // � ����Ʈ����

    private TextMeshProUGUI _questRewardCountText; // ���� ����
    public RectTransform RewardPos => _questRewardCountText.rectTransform; // ���� ����

    private Image _questRewardTypeImg; //� ��������

    private CanvasGroup _contentsCanvasGroup;
    private CanvasGroup _startButtonCanvasGroup;

    private Button _questStartButton;

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

        SignalHub.OnOffPopUiEvent += OffCanvasGroups;
    }

    public void UpdatePopUpQuestUI(QuestData questData)
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
        switch (questState)
        {
            case QuestState.Locked:
                questStateText = "���� �Ұ���";
                break;
            case QuestState.CanStart:
                questStateText = "���� ����";
                break;
            case QuestState.Running:
                questStateText = "���� ��";
                break;
            case QuestState.Finish:
                questStateText = "�Ϸ�";
                break;
        }

        _questStateText.SetText(questStateText);

        _questNameText.SetText(questName);
        _questContentText.SetText(questContent);
        _questRewardTypeImg.sprite = questRewardTypeImg;
        _questRewardCountText.SetText(questRewardCount.ToString());
        UpdateProgressText($"{questData.CurProgressCount} / {questData.RepeatCount}");

        _questStartButton.onClick.AddListener(() => StartQuest(questName));
    }

    public void StartQuest(string questName)
    {
        QuestManager.Instance.StartQuest(questName);
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
