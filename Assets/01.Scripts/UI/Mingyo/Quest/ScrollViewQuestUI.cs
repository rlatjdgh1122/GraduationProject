using System;
using TMPro;
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
    private Sprite _exclamationMarkSprite, _questionMarkSprite, _checkMarkSprite;

    [SerializeField]
    private Color _questionBoxColor, _exclamationBoxColor, _checkMarkBoxColor;

    [SerializeField]
    private Image _questStateImgBox;

    public void SetUpScrollViewUI(string questNameText, QuestState questState, Action action)
    {
        _button.onClick.RemoveAllListeners();
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
            case QuestState.CanFinish:
                questTypeImg.sprite = _checkMarkSprite;
                questTypeImg.color = Color.white;
                _questStateImgBox.color = _checkMarkBoxColor;
                break;
            case QuestState.Finish:
                questTypeImg.sprite = _checkMarkSprite;
                questTypeImg.color = Color.white;
                _questStateImgBox.color = _checkMarkBoxColor;
                break;
                default: break;
        }
    }
}
