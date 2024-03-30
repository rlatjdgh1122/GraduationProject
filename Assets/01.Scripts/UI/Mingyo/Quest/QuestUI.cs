using AssetKits.ParticleImage.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : PopupUI
{
    private Transform _questPopupContentsParentTrm;

    private QuestInfoUI _questInfoUI;
    public QuestInfoUI QuestInfoUICompo => _questInfoUI;

    [SerializeField]
    private GameObject _questUIPrefabs;

    private Dictionary<string, ScrollViewQuestUI> _uncompletedQuestScrollViewUIs = new Dictionary<string, ScrollViewQuestUI>();
    private int _unStartedQuestsLength;

    private Image _questStateIcon;
    private Image _questStateBox;

    [SerializeField]
    private Sprite _exclamationMarkSprite, _checkMarkSprite;

    [SerializeField]
    private Color _exclamationBoxColor, _checkMarkBoxColor;

    public override void Awake()
    {
        base.Awake();

        _questPopupContentsParentTrm = transform.Find("QuestList/UncompletedQuest/PopUp/ScrollView/Viewport/Content");

        _questInfoUI = transform.Find("QuestList/UncompletedQuest/ContentPopUp").GetComponent<QuestInfoUI>();

        Transform questButton = transform.parent.Find("MainInterfaceUI/QuestButton");
        questButton.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.ShowPanel("QuestUI"));

        _questStateIcon = questButton.Find("CautionBox").GetChild(0).GetComponent<Image>();
        _questStateBox = questButton.Find("CautionBox").GetComponent<Image>();

        _uncompletedQuestScrollViewUIs.Clear();

        SetCautionBoxImage(false, true);
    }

    public void CreateScrollViewUI(QuestData questData)
    {
        if (!_uncompletedQuestScrollViewUIs.ContainsKey(questData.Id))
        {
            GameObject newQuestObj = Instantiate(_questUIPrefabs, _questPopupContentsParentTrm);
            ScrollViewQuestUI newQuest = newQuestObj.GetComponent<ScrollViewQuestUI>();
            newQuest.SetUpScrollViewUI(questData.Id, QuestState.CanStart,
                () => UpdatePopUpQuestUI(questData));

            _uncompletedQuestScrollViewUIs.Add(questData.Id, newQuest);
            _unStartedQuestsLength++;


            SetCautionBoxImage(false);
        }
    }

    public void SetCautionBoxImage(bool isEnd, bool isNull = false)
    {
        _questStateIcon.enabled = true;

        if (isNull)
        {
            _questStateIcon.enabled = false;
            _questStateBox.color = new Color(0, 0, 0, 0);
            return;
        }

        if (isEnd)
        {
            _questStateIcon.sprite = _checkMarkSprite;
            _questStateBox.color = _checkMarkBoxColor;
        }
        else
        {
            _questStateIcon.sprite = _exclamationMarkSprite;
            _questStateBox.color = _exclamationBoxColor;
        }
    }

    private QuestData _currentQuestData;
    public void UpdatePopUpQuestUI(QuestData questData)
    {
        _currentQuestData = questData;

        _questInfoUI.UpdatePopUpQuestUI(_currentQuestData, () => SetCautionBoxImage(false, true));
        _uncompletedQuestScrollViewUIs[_currentQuestData.Id].UpdateQuestType(_currentQuestData.QuestStateEnum);

        // 기존에 등록된 이벤트 핸들러 제거
        SignalHub.OnStartQuestEvent -= OnStartQuestEventHandler;

        // 새로운 이벤트 핸들러 등록
        SignalHub.OnStartQuestEvent += OnStartQuestEventHandler;
    }


    private void OnStartQuestEventHandler()
    {
        SetQuestUIToRunning(_uncompletedQuestScrollViewUIs[_currentQuestData.Id]);
    }

    public void UpdateQuestUIToProgress(QuestData questData)
    {
        _questInfoUI.UpdateProgressText($"{questData.CurProgressCount} / {questData.RepeatCount}");
    }

    private void SetQuestUIToRunning(ScrollViewQuestUI quest)
    {
        quest.UpdateQuestType(QuestState.Running);
        _unStartedQuestsLength--;

        if (_unStartedQuestsLength == 0)
        {
            SetCautionBoxImage(false, true);
        }
    }

    public void RemoveQuestContentUI(string id)
    {
        Destroy(_uncompletedQuestScrollViewUIs[id].gameObject);
        _uncompletedQuestScrollViewUIs.Remove(id);
    }

    public void OpenCompletedQuests() //이거는 나중에 PopupPanel상속받는 거기서 할거임
    {
        UIManager.Instance.ShowPanel("CompletedQuest");
        UIManager.Instance.HidePanel("UncompletedQuest");
    }

    public void OpenUnCompletedQuests()
    {
        UIManager.Instance.ShowPanel("UncompletedQuest");
        UIManager.Instance.HidePanel("CompletedQuest");
    }
}
