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

    private TextMeshProUGUI _questRewardCountText; // 보상 갯수

    private Image _questRewardTypeImg; //어떤 보상인지

    private CanvasGroup _contentsCanvasGroup;

    private void Awake()
    {
        Transform contents = transform.Find("Contents").transform;

        _contentsCanvasGroup = contents.GetComponent<CanvasGroup>();

        Transform popUpBox = contents.Find("BoxText").transform;

        _questStateText = popUpBox.Find("State").GetComponent<TextMeshProUGUI>();

        _questNameText = popUpBox.Find("QuestName").GetComponent<TextMeshProUGUI>();

        _questContentText = popUpBox.Find("Description01").GetComponent<TextMeshProUGUI>();



        Transform rewardBox = contents.Find("BoxReward").transform;

        _questRewardTypeImg = rewardBox.Find("key").GetComponent<Image>();

        _questRewardCountText = rewardBox.Find("key/rewardCount").GetComponent<TextMeshProUGUI>();

        SignalHub.OnOffPopUiEvent += () => _contentsCanvasGroup.alpha = 0;
    }

    public void UpdatePopUpQuestUI(QuestState questState, string questName, string questContent,
                                   Sprite questRewardTypeImg, int questRewardCount)
    {
        _contentsCanvasGroup.alpha = 1;
        string questStateText = null;
        switch (questState)
        {
            case QuestState.BeforeStart:
                questStateText = "시작 가능";
                break;
            case QuestState.Running:
                questStateText = "진행 중";
                break;
            case QuestState.Finish:
                questStateText = "완료";
                break;
        }

        _questStateText.SetText(questStateText);

        _questNameText.SetText(questName);
        _questContentText.SetText(questContent);
        _questRewardTypeImg.sprite = questRewardTypeImg;
        _questRewardCountText.SetText(questRewardCount.ToString());
    }


    public void QuestStartBtn(string questId)
    {
        QuestManager.Instance.StartQuest(questId);

        SignalHub.OnStartQuestEvent?.Invoke();
    }

    private void OnEnable()
    {
        SignalHub.OnOffPopUiEvent -= () => _contentsCanvasGroup.alpha = 0;
    }
}
