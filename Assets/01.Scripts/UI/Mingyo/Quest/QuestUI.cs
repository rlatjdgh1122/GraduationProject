using AssetKits.ParticleImage.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : PopupUI
{
    private Transform _questPopupContentsParentTrm;

    private QuestInfoUI _questInfoUI;
    public QuestInfoUI QuestInfoUICompo => _questInfoUI;

    [SerializeField]
    private GameObject _questUIPrefabs;

    private Dictionary<string, GameObject> _scrollViewQuestUIsDic = new Dictionary<string, GameObject>();

    private GameObject _startableQuestIcon;

    public override void Awake()
    {
        base.Awake();

        _questPopupContentsParentTrm = transform.Find("QuestList/PopUp/ScrollView/Viewport/Content").gameObject.transform;

        _questInfoUI = transform.Find("QuestList/ContentPopUp").GetComponent<QuestInfoUI>();

        Transform questButton = transform.parent.Find("CostUI/QuestButton");
        questButton.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.ShowPanel("QuestUI"));

        _startableQuestIcon = questButton.Find("CautionBox").gameObject;
        _startableQuestIcon.SetActive(false);
    }

    public void CreateScrollViewUI(QuestData questData)
    {
        GameObject newQuestObj = Instantiate(_questUIPrefabs, _questPopupContentsParentTrm);
        ScrollViewQuestUI newQuest = newQuestObj.GetComponent<ScrollViewQuestUI>();
        newQuest.SetUpScrollViewUI(questData.Id, QuestState.CanStart,
            () => UpdatePopUpQuestUI(questData));

        _scrollViewQuestUIsDic.Add(questData.Id, newQuestObj);

        SignalHub.OnStartQuestEvent += () => newQuest.UpdateQuestType(QuestState.Running);

        if (!_startableQuestIcon.activeInHierarchy)
        {
            _startableQuestIcon.SetActive(true);
        }
    }

    public void UpdatePopUpQuestUI(QuestData questData)
    {
        if (_scrollViewQuestUIsDic.Count == 0)
        {
            _startableQuestIcon.SetActive(false);
        }
        _questInfoUI.UpdatePopUpQuestUI(questData);
    }

    public void RemoveQuestContentUI(string id)
    {
        Destroy(_scrollViewQuestUIsDic[id]);
        _scrollViewQuestUIsDic.Remove(id);
    }

    public void UpdateQuestUIToProgress(QuestData questData)
    {
        _questInfoUI.UpdateProgressText($"{questData.CurProgressCount} / {questData.RepeatCount}");
    }
}
