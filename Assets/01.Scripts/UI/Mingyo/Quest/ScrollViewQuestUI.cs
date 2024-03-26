using System;
using System.Collections;
using System.Collections.Generic;
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
