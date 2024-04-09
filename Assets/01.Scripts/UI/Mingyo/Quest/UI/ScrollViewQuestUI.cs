using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewQuestUI : MonoBehaviour
{
    [SerializeField]
    private Button _button; // 스크롤뷰에서 누를 버튼. 누르면 오른쪽에 이 퀘스트 정보가 딱 뜸
    [SerializeField]
    private Image questTypeImg; // 퀘스트 타입 이미지. 일단 이거 설정하는 기능은 안 만들어져 있다.
    [SerializeField]
    private TextMeshProUGUI _questNameText; // 퀘스트 이름. so에서 id로 지정한 거로 하면 됨

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
