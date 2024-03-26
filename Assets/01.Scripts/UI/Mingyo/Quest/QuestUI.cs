using AssetKits.ParticleImage.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : PopupUI
{
    private Transform _questPopupContentsParentTrm;

    private QuestInfoUI _questInfoUI;

    [SerializeField]
    private GameObject _questUIPrefabs;

    private Dictionary<string, GameObject> _scrollViewQuestUIsDic = new Dictionary<string, GameObject>();

    public override void Awake()
    {
        base.Awake();

        _questPopupContentsParentTrm = transform.Find("QuestList/PopUp/ScrollView/Viewport/Content").gameObject.transform;

        _questInfoUI = transform.Find("QuestList/QuestName/PopUp").GetComponent<QuestInfoUI>();
    }

    public void CreateScrollViewUI(QuestData questData)
    {
        GameObject newQuestObj = Instantiate(_questUIPrefabs, _questPopupContentsParentTrm);
        ScrollViewQuestUI newQuest = newQuestObj.GetComponent< ScrollViewQuestUI>();
        newQuest.SetUpScrollViewUI(questData.Id,
            () => UpdatePopUpQuestUI(questData));

        _scrollViewQuestUIsDic.Add(questData.Id, newQuestObj);
    }

    public void UpdatePopUpQuestUI(QuestData questData)
    {
        _questInfoUI.UpdatePopUpQuestUI(questData);
    }

    public void RemoveQuestContentUI(string id)
    {
        Destroy(_scrollViewQuestUIsDic[id]);
        _scrollViewQuestUIsDic.Remove(id);
    }

    public void UpdateProgressText(QuestData questData)
    {
        _questInfoUI.UpdateProgressText($"{questData.CurProgressCount} / {questData.RepeatCount}");
    }
}
