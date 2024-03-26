using System;
using System.Collections;
using System.Collections.Generic;
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

    public void SetUpScrollViewUI(/*Image questTypeImg*/ string questNameText, Action action)
    {
        _questNameText.SetText(questNameText);

        _button.onClick.AddListener(() => action());
    }

    public void UpdateQuestType(Sprite sprite)
    {
        questTypeImg.sprite = sprite; 
    }
}
