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
    private Transform _completedQuestsPopupContentsParentTrm;

    private QuestInfoUI _questInfoUI;
    public QuestInfoUI QuestInfoUICompo => _questInfoUI;

    private QuestInfoUI _completedQuestInfoUI;

    [SerializeField]
    private GameObject _questUIPrefabs;

    private Dictionary<string, ScrollViewQuestUI> _uncompletedQuestScrollViewUIs = new Dictionary<string, ScrollViewQuestUI>();
    private Dictionary<string, ScrollViewQuestUI> _completedQuestScrollViewUIs = new Dictionary<string, ScrollViewQuestUI>();
    private int _unStartedQuestsLength;

    private Image _questStateIcon;
    private Image _questStateBox;

    [SerializeField]
    private Sprite _exclamationMarkSprite, _checkMarkSprite;

    [SerializeField]
    private Color _exclamationBoxColor, _checkMarkBoxColor;

    private CanvasGroup _completedQuestsCanvasGroup;
    public override void Awake()
    {
        base.Awake();

        _questPopupContentsParentTrm = transform.Find("QuestList/UncompletedQuest/PopUp/ScrollView/Viewport/Content");

        _completedQuestsPopupContentsParentTrm = transform.Find("QuestList/CompletedQuest/PopUp/ScrollView/Viewport/Content");
        _completedQuestInfoUI = transform.Find("QuestList/CompletedQuest/ContentPopUp").GetComponent<QuestInfoUI>();

        _questInfoUI = transform.Find("QuestList/UncompletedQuest/ContentPopUp").GetComponent<QuestInfoUI>();

        Transform questButton = transform.parent.Find("MainInterfaceUI/QuestButton");
        questButton.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.ShowPanel("QuestUI"));

        _questStateIcon = questButton.Find("CautionBox").GetChild(0).GetComponent<Image>();
        _questStateBox = questButton.Find("CautionBox").GetComponent<Image>();

        _completedQuestsCanvasGroup = transform.Find("QuestList/CompletedQuest").GetComponent<CanvasGroup>();

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
    private QuestData _finishQuestData;
    public void UpdatePopUpQuestUI(QuestData questData, bool isFinished = false)
    {
        if (isFinished) { _finishQuestData = questData; }
        else { _currentQuestData = questData; }

        if (!isFinished)
        {
            _questInfoUI.UpdatePopUpQuestUI(_currentQuestData, () => SetCautionBoxImage(false, true));
            _uncompletedQuestScrollViewUIs[_currentQuestData.Id].UpdateQuestType(_currentQuestData.QuestStateEnum);
        }
        else
        {
            _completedQuestInfoUI.UpdatePopUpQuestUI(_finishQuestData, () => SetCautionBoxImage(true, true));
            _completedQuestScrollViewUIs[_finishQuestData.Id].UpdateQuestType(_finishQuestData.QuestStateEnum);
        }


        // 기존에 등록된 이벤트 핸들러 제거
        SignalHub.OnStartQuestEvent -= OnStartQuestEventHandler;

        // 새로운 이벤트 핸들러 등록
        SignalHub.OnStartQuestEvent += OnStartQuestEventHandler;
    }


    private void OnStartQuestEventHandler()
    {
        SetQuestUIToRunning(_uncompletedQuestScrollViewUIs[_currentQuestData.Id]);
    }

    public void AddCompletedQuestUI(string id)
    {
        QuestData questData = QuestManager.Instance.GetQuestData(id);
        _completedQuestScrollViewUIs.Add(id, _uncompletedQuestScrollViewUIs[id]);
        _uncompletedQuestScrollViewUIs.Remove(id);

        _completedQuestScrollViewUIs[id].SetUpScrollViewUI(id, QuestState.Finish,
                () => UpdatePopUpQuestUI(questData, true));

        _completedQuestScrollViewUIs[id].transform.SetParent(_completedQuestsPopupContentsParentTrm);
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
