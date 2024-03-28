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

        _questPopupContentsParentTrm = transform.Find("QuestList/PopUp/ScrollView/Viewport/Content").gameObject.transform;

        _questInfoUI = transform.Find("QuestList/ContentPopUp").GetComponent<QuestInfoUI>();

        Transform questButton = transform.parent.Find("CostUI/QuestButton");
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

    public void UpdatePopUpQuestUI(QuestData questData)
    {
        _questInfoUI.UpdatePopUpQuestUI(questData, () => SetCautionBoxImage(false, true));


        if (_uncompletedQuestScrollViewUIs.ContainsKey(questData.Id))
        {
            _uncompletedQuestScrollViewUIs[questData.Id].UpdateQuestType(questData.QuestStateEnum);
            SignalHub.OnStartQuestEvent += () => SetQuestUIToRunning(_uncompletedQuestScrollViewUIs[questData.Id]);
        }
        else
        {
            Debug.Log(questData.Id);
        }
    }

    public void RemoveQuestContentUI(string id)
    {
        Destroy(_uncompletedQuestScrollViewUIs[id].gameObject);
        _uncompletedQuestScrollViewUIs.Remove(id);
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
}
