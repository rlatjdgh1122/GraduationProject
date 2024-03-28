using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewQuestUI : MonoBehaviour
{
    [SerializeField]
    private Button _button; // ��ũ�Ѻ信�� ���� ��ư. ������ �����ʿ� �� ����Ʈ ������ �� ��
    [SerializeField]
    private Image questTypeImg; // ����Ʈ Ÿ�� �̹���. �ϴ� �̰� �����ϴ� ����� �� ������� �ִ�.
    [SerializeField]
    private TextMeshProUGUI _questNameText; // ����Ʈ �̸�. so���� id�� ������ �ŷ� �ϸ� ��

    [SerializeField]
    private Sprite _exclamationMarkSprite, _questionMarkSprite;

    [SerializeField]
    private Color _questionBoxColor, _exclamationBoxColor;

    [SerializeField]
    private Image _questStateImgBox;

    public void SetUpScrollViewUI(string questNameText, QuestState questState, Action action)
    {
        UpdateQuestType(questState);
        _questNameText.SetText(questNameText);

        _button.onClick.AddListener(() => action());
    }

    public void UpdateQuestType(QuestState questState)
    {
        switch (questState)
        {
            case QuestState.CanStart:
                questTypeImg.sprite = _exclamationMarkSprite;
                questTypeImg.color = Color.yellow;
                _questStateImgBox.color = _exclamationBoxColor;
                break;
            case QuestState.Running:
                questTypeImg.sprite = _questionMarkSprite;
                questTypeImg.color = Color.red;
                _questStateImgBox.color = _questionBoxColor;
                break;
                default: break;
        }
    }

    private void OnEnable()
    {
        SignalHub.OnStartQuestEvent -= () => UpdateQuestType(QuestState.Running);
    }
}
